
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppControle.Shared.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Name { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição")]
        [MaxLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Preço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Estoque")]
        public float Stock { get; set; } = 0;

        public ICollection<ProductCategory>? lProductCategories { get; set; }

        public ICollection<ClientService>? lClientServices { get; set; }

        [Display(Name = "Serviços")]
        public ICollection<ProductImage>? lProductImages { get; set; }


        [Display(Name = "Imagem")]
        public string MainImage => (lProductImages == null || lProductImages.Count == 0) ? string.Empty : lProductImages.FirstOrDefault()!.Image;

        //public ICollection<TemporalSale>? TemporalSales { get; set; }

        //public ICollection<SaleDetail>? SaleDetails { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsService { get; set; }

        public string? UserId { get; set; }

        public User? User { get; set; }
        #region Informações do Produto
        //[DisplayFormat(DataFormatString = "{0:N2}")]
        //[Display(Name = "Estoque")]
        //public float Stock { get; set; } = 0;

        //[DisplayFormat(DataFormatString = "{0:N2}")]
        //[Display(Name = "Estoque Minimo")]
        //[Required(ErrorMessage = "O campo {0} é obrigatório.")]
        //public float Stock { get; set; }
        #endregion

        #region Info. Adicionais


        //[Required]
        //[Column(TypeName = "decimal(10,2)")]
        //public decimal vTrib { get; set; }

        //[StringLength(14)]
        //public string GtinTrib { get; set; }

        //[StringLength(6)]
        //public string uTrib { get; set; }

        //public bool isMultiply { get; set; }

        //[Required]
        //[Column(TypeName = "decimal(10,2)")]
        //public decimal qCom { get; set; }

        //[Column(TypeName = "decimal(10,2)")]
        //public decimal qTrib { get; set; }

        //[StringLength(14)]
        //public string CNPJManufacture { get; set; }

        //public bool RelevantScale { get; set; }

        //public indEscala IndicatorRelevantScale { get; set; }

        //[Required]
        //[Column(TypeName = "decimal(10,2)")]
        //public decimal vFreight { get; set; }

        //[Column(TypeName = "decimal(10,2)")]
        //public decimal vOtherExpenses { get; set; }

        //[Column(TypeName = "decimal(10,2)")]
        //public decimal vSafe { get; set; }

        //[Column(TypeName = "decimal(10,2)")]
        //public decimal vDiscount { get; set; }

        //[StringLength(10)]
        //public string CodBenIcms { get; set; }

        ////public Sped Sped { get; set; }

        //[StringLength(7)]
        //public string Cest { get; set; }

        //[StringLength(8)]
        //public string Ncm { get; set; }

        //[StringLength(7)]
        //public string Anp { get; set; }

        //[StringLength(3)]
        //public string exTipi { get; set; }

        //[StringLength(14)]
        //public string Gtin { get; set; }
        //[StringLength(6)]
        //public string UCom { get; set; }
        //public DateTime DiscountEndDate { get; set; }

        //[StringLength(50)]
        //public string Location { get; set; }

        //public bool ActiveBalance { get; set; }

        //public int CfopSellIn { get; set; }
        //public int CfopSellOut { get; set; }
        //public int CfopBuyOut { get; set; }
        //public int CfopBuyIn { get; set; }

        //[Column(TypeName = "decimal(10,2)")]
        //public decimal PesoBruto { get; set; }

        //[Column(TypeName = "decimal(10,2)")]
        //public decimal PesoLiquido { get; set; }

        //[Required]
        //public bool Ativo { get; set; }
        #endregion

        #region Info. Trib

        //public OrigemMercadoria Origem { get; set; }

        //public Csosnicms Csosn { get; set; }

        //public Csticms Cst { get; set; }
        ////ICMS

        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqicmsDeferred { get; set; }

        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqicmsFcpDeferred { get; set; }

        ////public List<ICMSprod> LIcms { get; set; } = new List<ICMSprod>();

        //public DeterminacaoBaseIcms ModBCIcms { get; set; }

        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqIcms { get; set; }
        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqFcp { get; set; }
        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqIcmsSt { get; set; }
        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqFcpSt { get; set; }
        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqCredIcms { get; set; }

        //public DeterminacaoBaseIcmsSt ModBCIcmsSt { get; set; }
        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqRedIcms { get; set; }
        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqReducaoIcmsSt { get; set; }
        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqOpracaoPropria { get; set; }

        //public MotivoDesoneracaoIcms? MotivoDesoneracaoIcms { get; set; }

        //public Estado UfIcmsStDevido { get; set; }

        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqMva { get; set; }

        //////IPI
        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqIpi { get; set; }

        //public int CodEnqIpi { get; set; }

        //public CSTIPI? CstIpi { get; set; }

        //[StringLength(14)]
        //public string CnpjProdutorIpi { get; set; }

        ////PIS
        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqPis { get; set; }

        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqPisSt { get; set; }

        //public CSTPIS CstPis { get; set; }

        //////COFINS
        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqCofins { get; set; }

        //[Column(TypeName = "decimal(10,2)")]
        //public decimal AliqCofinsSt { get; set; }

        //public CSTCOFINS CstCofins { get; set; }

        ////public ProdutoEspecifico SpecificProduct { get; set; }

        #endregion

    }
}
