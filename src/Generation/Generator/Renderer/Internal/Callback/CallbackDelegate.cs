﻿using System.Collections.Generic;
using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class CallbackDelegate
{
    public static string Render(GirModel.Callback callback)
    {
        return $@"
using System;
using System.Runtime.InteropServices;

#nullable enable

namespace {Namespace.GetInternalName(callback.Namespace)}
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    public delegate {ReturnType.RenderForCallback(callback.ReturnType)} {callback.Name}({RenderParameters(callback.Parameters)});
}}";
    }

    private static string RenderParameters(IEnumerable<GirModel.Parameter> parameters)
    {
        return parameters
                .Select(RenderableCallbackParameterFactory.Create)
                .Select(parameter => $@"{parameter.Attribute}{parameter.Direction}{parameter.NullableTypeName} {parameter.Name}")
                .Join(", ");
    }
}
