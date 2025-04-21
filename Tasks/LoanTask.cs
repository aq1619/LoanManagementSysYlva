using LoanManagementSys.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSys.Tasks
{
    internal class LoanTask
    {
        public ProductManager productManager;
        public MemberManager memberManager;
        public LoanItemManager loanItemManager;
        private Random random;
        //public Product product;
        public LoanSystem loanSystem;

        private bool isRunning = true;

        public LoanTask(ProductManager productManager,MemberManager memberManager, LoanItemManager loanItemManager, LoanSystem loanSystem)
        {
            this.productManager = productManager;
            this.memberManager = memberManager;
            this.loanItemManager = loanItemManager;
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
                    Thread.Sleep(random.Next(2000, 6000));
                    LoanItem();
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

        public void LoanItem()
        {

            if (productManager.Count == 0 || memberManager.Count == 0)
                return;

            int memberIndex = random.Next(memberManager.Count);
            int productIndex = random.Next(productManager.Count);

            Member selectedMember = memberManager.Get(memberIndex);
            Product selectedProduct = productManager.Get(productIndex);
            LoanItem loanItem = new LoanItem(selectedProduct, selectedMember);

            productManager.Remove(productIndex);
            loanItemManager.Add(loanItem);
            string eventMessage = $"Member '{selectedMember.Name}' loaned product: {selectedProduct.Name}";
            loanSystem.WriteEventLog(eventMessage);

        }
    }
}
