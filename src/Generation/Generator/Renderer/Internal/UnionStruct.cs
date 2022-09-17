﻿using System;
using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class UnionStruct
{
    public static string Render(GirModel.Union union)
    {
        return $@"
using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

#nullable enable

namespace {Namespace.GetInternalName(union.Namespace)}
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    {PlatformSupportAttribute.Render(union as GirModel.PlatformDependent)}
    [StructLayout(LayoutKind.Explicit)]
    public partial struct {Union.GetInternalStructName(union)}
    {{
        {union.Fields
            .Select(RenderField)
            .Join(Environment.NewLine)}
    }}
}}";
    }

    private static string RenderField(GirModel.Field field)
    {
        var renderableField = RenderableFieldFactory.Create(field);

        return @$"[FieldOffset(0)]{renderableField.Attribute} public {renderableField.NullableTypeName} {renderableField.Name};";
    }
}