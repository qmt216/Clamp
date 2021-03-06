﻿using Clamp.OSGI.Framework.Data.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Xml;

namespace Clamp.OSGI.Framework.Data.Description
{
    public sealed class ExtensionPoint : ObjectDescription
    {
        private string path;
        private string name;
        private string description;
        private ExtensionNodeSet nodeSet;
        private ConditionTypeDescriptionCollection conditions;
        private string defaultInsertBefore;
        private string defaultInsertAfter;

        // Information gathered from others addins:

        private List<string> addins;  // Add-ins which extend this extension point
        private string rootBundle;     // Add-in which defines this extension point

        internal ExtensionPoint(XmlElement elem) : base(elem)
        {
            path = elem.GetAttribute("path");
            name = elem.GetAttribute("name");
            defaultInsertBefore = elem.GetAttribute("defaultInsertBefore");
            defaultInsertAfter = elem.GetAttribute("defaultInsertAfter");
            description = ReadXmlDescription();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mono.Bundles.Description.ExtensionPoint"/> class.
        /// </summary>
        public ExtensionPoint()
        {
        }

        /// <summary>
        /// Copies another extension point.
        /// </summary>
        /// <param name='ep'>
        /// Extension point from which to copy.
        /// </param>
        public void CopyFrom(ExtensionPoint ep)
        {
            path = ep.path;
            name = ep.name;
            defaultInsertBefore = ep.defaultInsertBefore;
            defaultInsertAfter = ep.defaultInsertAfter;
            description = ep.description;
            NodeSet.CopyFrom(ep.NodeSet);
            Conditions.Clear();
            foreach (ConditionTypeDescription cond in ep.Conditions)
            {
                ConditionTypeDescription cc = new ConditionTypeDescription();
                cc.CopyFrom(cond);
                Conditions.Add(cc);
            }
            Bundles.Clear();
            foreach (string s in ep.Bundles)
                Bundles.Add(s);
            rootBundle = ep.rootBundle;
        }

        internal override void Verify(string location, StringCollection errors)
        {
            VerifyNotEmpty(location + "ExtensionPoint", errors, Path, "path");
            NodeSet.Verify(location + "ExtensionPoint (" + Path + ")/", errors);
            Conditions.Verify(location + "ExtensionPoint (" + Path + ")/", errors);
        }

        internal void SetExtensionsBundleId(string addinId)
        {
            NodeSet.SetExtensionsBundleId(addinId);
            foreach (ConditionTypeDescription cond in Conditions)
                cond.BundleId = addinId;
            Bundles.Add(addinId);
        }

        internal void MergeWith(string thisBundleId, ExtensionPoint ep)
        {
            NodeSet.MergeWith(thisBundleId, ep.NodeSet);

            foreach (ConditionTypeDescription cond in ep.Conditions)
            {
                if (cond.BundleId != thisBundleId && !Conditions.Contains(cond))
                    Conditions.Add(cond);
            }
            foreach (string s in ep.Bundles)
            {
                if (!Bundles.Contains(s))
                    Bundles.Add(s);
            }
        }

        internal void UnmergeExternalData(string thisBundleId, Hashtable addinsToUnmerge)
        {
            NodeSet.UnmergeExternalData(thisBundleId, addinsToUnmerge);

            ArrayList todel = new ArrayList();
            foreach (ConditionTypeDescription cond in Conditions)
            {
                if (cond.BundleId != thisBundleId && (addinsToUnmerge == null || addinsToUnmerge.Contains(cond.BundleId)))
                    todel.Add(cond);
            }
            foreach (ConditionTypeDescription cond in todel)
                Conditions.Remove(cond);

            if (addinsToUnmerge == null)
                Bundles.Clear();
            else
            {
                foreach (string s in addinsToUnmerge.Keys)
                    Bundles.Remove(s);
            }
            if (thisBundleId != null && !Bundles.Contains(thisBundleId))
                Bundles.Add(thisBundleId);
        }

        internal void Clear()
        {
            NodeSet.Clear();
            Conditions.Clear();
            Bundles.Clear();
        }

        internal override void SaveXml(XmlElement parent)
        {
            CreateElement(parent, "ExtensionPoint");

            Element.SetAttribute("path", Path);

            if (Name.Length > 0)
                Element.SetAttribute("name", Name);
            else
                Element.RemoveAttribute("name");

            if (DefaultInsertBefore.Length > 0)
                Element.SetAttribute("defaultInsertBefore", DefaultInsertBefore);
            else
                Element.RemoveAttribute("defaultInsertBefore");

            if (DefaultInsertAfter.Length > 0)
                Element.SetAttribute("defaultInsertAfter", DefaultInsertAfter);
            else
                Element.RemoveAttribute("defaultInsertAfter");

            SaveXmlDescription(Description);

            if (nodeSet != null)
            {
                nodeSet.Element = Element;
                nodeSet.SaveXml(parent);
            }
        }

        /// <summary>
        /// Gets or sets the path that identifies the extension point.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path
        {
            get { return path != null ? path : string.Empty; }
            set { path = value; }
        }

        /// <summary>
        /// Gets or sets the display name of the extension point.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return name != null ? name : string.Empty; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the description of the extension point.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get { return description != null ? description : string.Empty; }
            set { description = value; }
        }

        /// <summary>
        /// Gets a list of add-ins that extend this extension point.
        /// </summary>
        /// <remarks>
        /// This value is only available when the add-in description is loaded from an add-in registry.
        /// </remarks>
        public string[] ExtenderBundles
        {
            get
            {
                return Bundles.ToArray();
            }
        }

        internal List<string> Bundles
        {
            get
            {
                if (addins == null)
                    addins = new List<string>();
                return addins;
            }
        }

        internal string RootBundle
        {
            get { return rootBundle; }
            set { rootBundle = value; }
        }

        /// <summary>
        /// A node set which specifies the node types allowed in this extension point.
        /// </summary>
        /// <value>
        /// The node set.
        /// </value>
        public ExtensionNodeSet NodeSet
        {
            get
            {
                if (nodeSet == null)
                {
                    if (Element != null)
                        nodeSet = new ExtensionNodeSet(Element);
                    else
                        nodeSet = new ExtensionNodeSet();
                    nodeSet.SetParent(this);
                }
                return nodeSet;
            }
        }

        internal void SetNodeSet(ExtensionNodeSet nset)
        {
            // Used only by the addin updater
            nodeSet = nset;
            nodeSet.SetParent(this);
        }

        /// <summary>
        /// Gets the conditions available in this node set.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
        public ConditionTypeDescriptionCollection Conditions
        {
            get
            {
                if (conditions == null)
                {
                    conditions = new ConditionTypeDescriptionCollection(this);
                    if (Element != null)
                    {
                        foreach (XmlElement elem in Element.SelectNodes("ConditionType"))
                            conditions.Add(new ConditionTypeDescription(elem));
                    }
                }
                return conditions;
            }
        }

        /// <summary>
        /// Adds an extension node type.
        /// </summary>
        /// <returns>
        /// The extension node type.
        /// </returns>
        /// <param name='name'>
        /// Name of the node
        /// </param>
        /// <param name='typeName'>
        /// Name of the type that implements the extension node.
        /// </param>
        /// <remarks>
        /// This method can be used to register a new allowed node type for the extension point.
        /// </remarks>
        public ExtensionNodeType AddExtensionNode(string name, string typeName)
        {
            ExtensionNodeType ntype = new ExtensionNodeType();
            ntype.Id = name;
            ntype.TypeName = typeName;
            NodeSet.NodeTypes.Add(ntype);
            return ntype;
        }

        /// <summary>
        /// The id of the extension before which new extensions will be added, unless the extension defines its own InsertBefore value
        /// </summary>
        /// <value>The default insert before.</value>
        public string DefaultInsertBefore
        {
            get { return defaultInsertBefore ?? ""; }
            set { defaultInsertBefore = value; }
        }

        /// <summary>
        /// The id of the extension after which new extensions will be added, unless the extension defines its own InsertAfter value
        /// </summary>
        /// <value>The default insert before.</value>
        public string DefaultInsertAfter
        {
            get { return defaultInsertAfter ?? ""; }
            set { defaultInsertAfter = value; }
        }

        internal override void Write(BinaryXmlWriter writer)
        {
            writer.WriteValue("path", path);
            writer.WriteValue("name", name);
            writer.WriteValue("description", Description);
            writer.WriteValue("rootBundle", rootBundle);
            writer.WriteValue("addins", Bundles);
            writer.WriteValue("NodeSet", NodeSet);
            writer.WriteValue("Conditions", Conditions);
            writer.WriteValue("defaultInsertBefore", defaultInsertBefore);
            writer.WriteValue("defaultInsertAfter", defaultInsertAfter);
        }

        internal override void Read(BinaryXmlReader reader)
        {
            path = reader.ReadStringValue("path");
            name = reader.ReadStringValue("name");
            if (!reader.IgnoreDescriptionData)
                description = reader.ReadStringValue("description");
            rootBundle = reader.ReadStringValue("rootBundle");
            addins = (List<string>)reader.ReadValue("addins", new List<string>());
            nodeSet = (ExtensionNodeSet)reader.ReadValue("NodeSet");
            conditions = (ConditionTypeDescriptionCollection)reader.ReadValue("Conditions", new ConditionTypeDescriptionCollection(this));
            defaultInsertBefore = reader.ReadStringValue("defaultInsertBefore");
            defaultInsertAfter = reader.ReadStringValue("defaultInsertAfter");
            if (nodeSet != null)
                nodeSet.SetParent(this);
        }
    }
}
