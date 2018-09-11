using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseLayer
{
    public class OcrDocumentPage
    {
        public OcrDocumentPage()
        {
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string PageName { get; set; }
        public byte[] Page { get; set; }
    }
}