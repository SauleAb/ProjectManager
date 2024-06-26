﻿using augalinga.Data.Access;
using augalinga.Data.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandscapeProjectsManager.MVVM.ViewModels
{
    public class OrdersViewModel
    {
        string _projectName;
        public OrdersViewModel(string projectName)
        {
            _projectName = projectName;
            LoadOrders(_projectName);
        }

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public void AddOrderToCollection(Order order)
        {
            Orders.Add(order);
            LoadOrders(_projectName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadOrders(string projectName)
        {
            using (var context = new DataContext())
            {
                var orders = context.Orders.Where(order => order.Project == projectName).ToList();

                Orders = new ObservableCollection<Order>(orders);
            }
        }

        public void RemoveOrder(string orderLink)
        {
            //local
            var orderToRemove = Orders.FirstOrDefault(p => p.Link == orderLink);
            Orders.Remove(orderToRemove);

            //database
            using (var dbContext = new DataContext())
            {
                dbContext.Orders.Remove(orderToRemove);
                dbContext.SaveChanges();
            }

            LoadOrders(_projectName);
        }
}
}
