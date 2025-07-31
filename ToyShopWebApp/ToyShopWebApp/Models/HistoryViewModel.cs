using ToyShopWebApp.Models;
using System;

namespace ToyShopWebApp.ViewModels
{
    public class HistoryViewModel
    {
        public Toy Toy { get; set; }
        public DateTime ViewedAt { get; set; }
    }
}
