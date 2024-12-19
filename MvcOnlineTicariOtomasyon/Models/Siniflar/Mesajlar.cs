using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
	public class Mesajlar
	{
		[Key]
		public int MesajId { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(30)]
		public string Gonderen { get; set; }


		[Column(TypeName = "Varchar")]
		[StringLength(30)]
		public string Gonderilen { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(30)]
		public string Konu { get; set; }


		[Column(TypeName = "Varchar")]
		[StringLength(2000)]
		public string icerik { get; set; }


		[Column(TypeName = "Smalldatetime")]
		public DateTime Tarih { get; set; }

	}
}