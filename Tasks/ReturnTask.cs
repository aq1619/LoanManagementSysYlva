using LoanManagementSys.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSys.Tasks
{
    internal class ReturnTask
    {
        public ProductManager productManager;
        public MemberManager memberManager;
        public LoanItemManager loanItemManager;
        private Random random;
        //public Product product;
        public LoanSystem loanSystem;

        private bool isRunning = true;

        public ReturnTask(ProductManager productManager, MemberManager memberManager, LoanItemManager loanItemManager, LoanSystem loanSystem)
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
                    Thread.Sleep(random.Next(5000, 8000));
                    ReturnItem();
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

        public void ReturnItem()
        {

            if (loanItemManager.Count == 0)
                return;

            int memberIndex = random.Next(memberManager.Count);
            int productIndex = random.Next(productManager.Count);
            int loanItemIndex = random.Next(loanItemManager.Count);

            LoanItem selectedLoanItem = loanItemManager.Get(loanItemIndex);
            Member selectedMember = selectedLoanItem.Member;
            Product selectedProduct = selectedLoanItem.Product;

            productManager.Add(selectedProduct);
            loanItemManager.Remove(loanItemIndex);
            string eventMessage = $"Member '{selectedMember.Name}' returned product: {selectedProduct.Name}";
            loanSystem.WriteEventLog(eventMessage);

        }
    }
}
