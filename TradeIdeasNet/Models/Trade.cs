
using System;
using System.ComponentModel.DataAnnotations;

namespace TradeIdeasNet.Models
{
    //Trade json model

    public class Trade
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Ticker { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        [Display(Name = "Trade Type")]
        public string Tradetype { get; set; }

        [Required]
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        public string ApplicationUserID { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}