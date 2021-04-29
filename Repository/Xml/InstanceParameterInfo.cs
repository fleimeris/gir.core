﻿using System.Xml.Serialization;

namespace Repository.Xml
{
    public class InstanceParameterInfo : Typed
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("transfer-ownership")]
        public string? TransferOwnership { get; set; }

        [XmlAttribute("direction")]
        public string? Direction { get; set; }

        [XmlAttribute("caller-allocates")]
        public bool CallerAllocates;

        [XmlElement("doc")]
        public DocInfo? Doc { get; set; }

        [XmlElement("type")]
        public TypeInfo? Type { get; set; }

        [XmlElement("array")]
        public ArrayInfo? Array { get; set; }

        [XmlAttribute("nullable")]
        public bool Nullable;
    }
}