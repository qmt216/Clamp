﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using Clamp.OSGI.Properties;
using Clamp.OSGI.DoozerImpl;
using Clamp.SDK.Doozer;

namespace Clamp.OSGI
{
    public class AddInTreeImpl : IAddInTree
    {
        private AddInTreeNode rootNode = new AddInTreeNode();
        private List<Bundle> addIns = new List<Bundle>();
        private ConcurrentDictionary<string, IDoozer> doozers = new ConcurrentDictionary<string, IDoozer>();
        private ConcurrentDictionary<string, IConditionEvaluator> conditionEvaluators = new ConcurrentDictionary<string, IConditionEvaluator>();

        public ConcurrentDictionary<string, IConditionEvaluator> ConditionEvaluators
        {
            get
            {
                return conditionEvaluators;
            }
        }

        public ReadOnlyCollection<Bundle> AddIns
        {
            get
            {
                return addIns.AsReadOnly();
            }
        }

        public ConcurrentDictionary<string, IDoozer> Doozers
        {
            get
            {
                return doozers;
            }
        }

        public AddInTreeImpl()
        {
            this.doozers.TryAdd("Class", new ClassDoozer());
            this.doozers.TryAdd("Webwork", new WebworkDoozer());
            this.doozers.TryAdd("Service", new ServiceDoozer());
            //conditionEvaluators.TryAdd("Compare", new CompareConditionEvaluator());
            //conditionEvaluators.TryAdd("Ownerstate", new OwnerStateConditionEvaluator());
        }

        public AddInTreeNode GetTreeNode(string path, bool throwOnNotFound = true)
        {
            if (path == null || path.Length == 0)
            {
                return rootNode;
            }
            string[] splittedPath = path.Split('/');
            AddInTreeNode curPath = rootNode;
            for (int i = 0; i < splittedPath.Length; i++)
            {
                if (!curPath.ChildNodes.TryGetValue(splittedPath[i].ToUpper(), out curPath))
                {
                    if (throwOnNotFound)
                        throw new AddInException(string.Format(StringResources.AddIn_TreePath_NotFound, path));
                    else
                        return null;
                }
            }
            return curPath;
        }


        public void InsertAddIn(Bundle addIn)
        {
            if (addIn.Enabled)
            {
                foreach (AddInFeature addInFeature in addIn.Features.Values)
                {
                    AddExtensionPath(addInFeature);
                }

                foreach (AddInRuntime runtime in addIn.Runtimes)
                {
                    if (runtime.IsActive)
                    {
                        foreach (var pair in runtime.DefinedDoozers)
                        {
                            if (!doozers.TryAdd(pair.Key, pair.Value))
                                throw new AddInException("Duplicate doozer: " + pair.Key);
                        }
                        foreach (var pair in runtime.DefinedConditionEvaluators)
                        {
                            if (!conditionEvaluators.TryAdd(pair.Key, pair.Value))
                                throw new AddInException("Duplicate condition evaluator: " + pair.Key);
                        }
                    }
                }

                string addInRoot = Path.GetDirectoryName(addIn.FileName);

                foreach (string bitmapResource in addIn.BitmapResources)
                {
                    string path = Path.Combine(addInRoot, bitmapResource);
                    ResourceManager resourceManager = ResourceManager.CreateFileBasedResourceManager(Path.GetFileNameWithoutExtension(path), Path.GetDirectoryName(path), null);
                    //ServiceSingleton.GetRequiredService<IResourceService>().RegisterNeutralImages(resourceManager);
                }

                foreach (string stringResource in addIn.StringResources)
                {
                    string path = Path.Combine(addInRoot, stringResource);
                    ResourceManager resourceManager = ResourceManager.CreateFileBasedResourceManager(Path.GetFileNameWithoutExtension(path), Path.GetDirectoryName(path), null);
                    //ServiceSingleton.GetRequiredService<IResourceService>().RegisterNeutralStrings(resourceManager);
                }
            }

            addIns.Add(addIn);
        }

        private void AddExtensionPath(AddInFeature path)
        {
            AddInTreeNode treePath = CreatePath(rootNode, path.Name);

            foreach (IEnumerable<Codon> innerCodons in path.GroupedCodons)
                treePath.AddCodons(innerCodons);
        }

        AddInTreeNode CreatePath(AddInTreeNode localRoot, string path)
        {
            if (path == null || path.Length == 0)
            {
                return localRoot;
            }

            string[] splittedPath = path.Split('/');

            AddInTreeNode curPath = localRoot;

            int i = 0;

            while (i < splittedPath.Length)
            {
                string keyValue = splittedPath[i]?.ToUpper();

                if (!curPath.ChildNodes.ContainsKey(keyValue))
                {
                    curPath.ChildNodes[keyValue] = new AddInTreeNode();
                }

                curPath = curPath.ChildNodes[keyValue];

                ++i;
            }

            return curPath;
        }

        private void DisableAddin(Bundle addIn, List<Bundle> enabledAddInsList, string mistake)
        {
            addIn.Enabled = false;

            addIn.Mistake = mistake;

            enabledAddInsList.Remove(addIn);
        }

        /// <summary>
        /// 加载插件树
        /// </summary>
        /// <param name="addInFiles"></param>
        /// <param name="disabledAddIns"></param>
        public void Load(List<string> addInFiles, List<string> disabledAddIns)
        {
            List<Bundle> addInslist = new List<Bundle>();
            List<Bundle> enabledAddInsList = new List<Bundle>();
            Dictionary<string, Bundle> addInDict = new Dictionary<string, Bundle>();

            var nameTable = new System.Xml.NameTable();

            foreach (string fileName in addInFiles)
            {
                Bundle addIn;

                try
                {
                    addIn = Bundle.Load(this, fileName, nameTable);
                }
                catch (AddInException ex)
                {
                    addIn = new Bundle(this);
                    addIn.FileName = fileName;
                    addIn.Enabled = false;
                    addIn.Mistake = ex.Message;
                }

                if (!addIn.Enabled)
                {
                    addInslist.Add(addIn);
                    continue;
                }

                addIn.Enabled = true;

                if (disabledAddIns != null && disabledAddIns.Count > 0)
                {
                    if (disabledAddIns.Contains(addIn.Name))
                    {
                        addIn.Enabled = false;
                        break;
                    }
                }

                if (addIn.Enabled)
                {
                    enabledAddInsList.Add(addIn);
                }

                addInslist.Add(addIn);
            }

            checkDependencies:

            for (int i = 0; i < addInslist.Count; i++)
            {
                Bundle addIn = addInslist[i];

                if (!addIn.Enabled)
                    continue;

                Bundle addInFound;

                foreach (AddInReference reference in addIn.Manifest.Conflicts)
                {
                    if (reference.Check(enabledAddInsList, out addInFound))
                    {
                        DisableAddin(addIn, enabledAddInsList, $"插件[{addIn.Name}]和插件[{addInFound.Name}]存在冲突");

                        //TODO 提示冲突的插件消息

                        goto checkDependencies;
                    }
                }

                foreach (AddInReference reference in addIn.Manifest.Dependencies)
                {
                    if (!reference.Check(enabledAddInsList, out addInFound))
                    {
                        DisableAddin(addIn, enabledAddInsList, $"插件[{addIn.Name}]的依赖插件[{reference.AddInId}]不存在");

                        //TODO 提示不可用的插件消息

                        goto checkDependencies;
                    }
                }
            }

            foreach (Bundle addIn in addInslist)
            {
                try
                {
                    InsertAddIn(addIn);
                }
                catch (AddInException ex)
                {
                }
            }

        }
    }
}