using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TradeIdeasNet.Models
{
    public class TradeFormViewModel
    {

        public int? Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Ticker { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        [Display(Name = "Trade Type")]
        public string Tradetype { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        public string ApplicationUserID { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }


        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Trade" : "New Trade";
            }
        }

        public TradeFormViewModel()
        {
            Id = 0;
        }

        public TradeFormViewModel(Trade trade)
        {
            Id = trade.Id;
            Ticker = trade.Ticker;
            Comment = trade.Comment;
            Tradetype = trade.Tradetype;
            DateAdded = trade.DateAdded;
        }
    }
}