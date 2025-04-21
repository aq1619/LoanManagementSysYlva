using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSys.Tasks
{
    public class AdminTask
    {
        public ProductManager productManager;
        private Random random;
        public Product product;
        public LoanSystem loanSystem;

        private bool isRunning = true;

        public AdminTask(ProductManager productManager, LoanSystem loanSystem)
        {
            this.productManager = productManager;
            this.loanSystem = loanSystem;
            random = new Random();
        }
        public bool IsRunning { get; set; }

        public void Run()
        {
            try
            {
                while (isRunning)
                {
                    Thread.Sleep(random.Next(6000, 16000));
                    newProduct();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Hello");
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void newProduct()
        {
            Product newProduct = productManager.AddNewTestProduct();
            string eventMessage = "Admin added product: " + newProduct.Name;
            loanSystem.WriteEventLog(eventMessage);
        }
    }
}
