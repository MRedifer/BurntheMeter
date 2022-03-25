using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //added for metadata and validation

namespace StoreFront.DATA.EF //.metadata
{

    #region Product MetaData
    public class ProductMetaData
    {
        public int ProductID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Can be up to 100 characters")]
        [Display(Name = "Product Name")]
        [UIHint("MultilineText")]
        public string ProductName { get; set; }

        //public int CategoryID { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Must be a valid number, 0 or larger")]
        [DisplayFormat(NullDisplayText = "[-N/A-]", DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        //public int StockStatusID { get; set; }

        //public Nullable<int> PlatformID { get; set; }

        //public Nullable<int> CableTypeID { get; set; }

        [UIHint("MultilineText")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string Description { get; set; }

        
        public string ProductImage { get; set; }
    }

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {

    }

    #endregion

    #region Category MetaData

    public class CategoryMetaData
    {
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "*Must be 20 characters or less")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "*Must be 50 characters or less")]
        [Display(Name = "Category Name")]
        [UIHint("MulilineText")]
        public string CategoryName { get; set; }
    }

    [MetadataType(typeof(CategoryMetaData))]
    public partial class Category { }

    #endregion

    #region StockStatu Metadata

    public class StockStatuMetaData
    {
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "*Can be up to 20 characters")]
        public int StockStatusID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "*Can be up to 50 characters")]
        public string StockStatus { get; set; }

        [StringLength(100, ErrorMessage = "*Can be up to 100 characters")]
        public string Notes { get; set; }
    }

    //[MetadataType(typeof(StockStatuMetaData))]
    //public partial class Category { }

    #endregion

    #region Platform MetaData

    public class PlatformMetaData
    {
        [Required]
        [StringLength(20, ErrorMessage = "*Can be up to 20 characters")]
        public int PlatformID { get; set; }

        [StringLength(50, ErrorMessage = "*Can be up to 50 characters")]
        public int PlatformName { get; set; }
    }

    [MetadataType(typeof(PlatformMetaData))]
    public partial class Platorm
    { }

    #endregion

    #region CableTypes Metadata

    public class CableTypeMetaData
    {
        [Required]
        [StringLength(20, ErrorMessage = "*Can be up to 20 characters")]
        public int CableTypeID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "*Can be up to 50 characters")]
        public string CableTypeName { get; set; }
    }

    [MetadataType(typeof(CableTypeMetaData))]
    public partial class CableType { }

    #endregion
}
