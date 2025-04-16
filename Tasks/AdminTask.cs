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
        private LoanSystem loanSystem;

        private bool isRunning = true;

        public AdminTask(ProductManager productManager)
        {
            this.productManager = productManager;
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


        private void newProduct()
        {
            productManager.AddNewTestProduct();
            Debug.WriteLine("New product added: " + productManager.Count);
            
            //   productManager.AddNewTestProduct();
        }

    }
}
