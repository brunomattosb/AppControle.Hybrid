using System.ComponentModel;
using System.Xml.Serialization;

namespace Library.Domain.Enums;
public enum GenderType
{
    [Description("Masculino")]
    [XmlEnum("1")]
    Masculino = 1,

    [Description("Feminino")]
    [XmlEnum("2")]
    Feminino = 2,
}
