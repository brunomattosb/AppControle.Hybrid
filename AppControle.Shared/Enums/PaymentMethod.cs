using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AppControle.Shared.Enums
{
    public enum PaymentMethod
    {
        [Description("Dinheiro")]
        [XmlEnum("0")]
        Dinheiro = 0,

        [Description("Débito")]
        [XmlEnum("1")]
        Debito = 1,

        [Description("Crédito")]
        [XmlEnum("2")]
        Credito = 2,

        [Description("Pix")]
        [XmlEnum("3")]
        Pix = 3,

        [Description("Fiado")]
        [XmlEnum("4")]
        Fiado = 4,
    }    
}
