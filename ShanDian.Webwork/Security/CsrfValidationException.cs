namespace ShanDian.Webwork.Security
{
    using System;

    public class CsrfValidationException : Exception
    {
        public CsrfTokenValidationResult Result { get; private set; }

        public CsrfValidationException(CsrfTokenValidationResult result)
            : base(result.ToString())
        {
            Result = result;
        }
    }
}