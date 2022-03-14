﻿namespace Generator3.Generation.Record
{
    public class InternalHandleTemplate : Template<InternalHandleModel>
    {
        public string Render(InternalHandleModel model)
        {
            return $@"
using System;
using System.Runtime.InteropServices;

#nullable enable

namespace { model.InternalNamespaceName }
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    public abstract class {model.HandleName} : SafeHandle
    {{
        protected {model.HandleName}(bool ownsHandle) : base(IntPtr.Zero, ownsHandle) {{ }}

        public sealed override bool IsInvalid => handle == IntPtr.Zero;
    }}

    public class {model.NullHandleName} : {model.HandleName}
    {{
        public static {model.NullHandleName} Instance = new {model.NullHandleName}();
    
        private {model.NullHandleName}() : base(true) {{ }}

        protected override bool ReleaseHandle()
        {{
            throw new System.Exception(""It is not allowed to free a \""{model.InternalNamespaceName}.{model.NullHandleName}\""."");
        }}
    }}

    public partial class {model.UnownedHandleName} : {model.HandleName}
    {{
        private {model.UnownedHandleName}() : base(false) {{ }}

        public {model.UnownedHandleName}(IntPtr handle) : base(false)
        {{
            SetHandle(handle);
        }}    

        protected override bool ReleaseHandle()
        {{
            throw new System.Exception(""It is not allowed to free a \""{model.InternalNamespaceName}.{model.UnownedHandleName}\""."");
        }}
    }}
}}";
        }
    }
}
