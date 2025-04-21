using LoanManagementSys.Managers;
using LoanManagementSys.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSys
{
    public class LoanSystem
    {
        private ProductManager productManager;
        private MemberManager memberManager;
        private LoanItemManager loanItemManager;


        private AdminTask adminTask;
        private UpdateGUI updateGUI;
        private ReturnTask returnTask;
        private LoanTask loanTask;

        private Thread adminThread;
        private Thread updateGUIThread;
        private Thread returnThread;
        private Thread loanThread;


        private ListBox productListBox;
        private ListBox eventLogListBox;
        public LoanSystem(ListBox productListBox, ListBox eventLogListBox)
        {
            productManager = new ProductManager();
            memberManager = new MemberManager();
            loanItemManager = new LoanItemManager();

            this.productListBox = productListBox;
            this.eventLogListBox = eventLogListBox;

            updateGUI = new UpdateGUI(this); 
            adminTask = new AdminTask(productManager, this);
            loanTask = new LoanTask(productManager, memberManager, loanItemManager, this);
            returnTask = new ReturnTask(productManager, memberManager, loanItemManager, this);

            productManager.AddTestProducts();
            memberManager.AddMembers();
        }

        public void Start()
        {
            adminThread = new Thread(new ThreadStart(adminTask.Run));
            adminThread.Start();

            updateGUIThread = new Thread(new ThreadStart(updateGUI.Run));
            updateGUIThread.Start();

            loanThread = new Thread(new ThreadStart(loanTask.Run));
            loanThread.Start();

            returnThread = new Thread(new ThreadStart(returnTask.Run));
            returnThread.Start();
        }
        public void Stop()
        {
            adminTask.IsRunning = false;
            updateGUI.IsRunning = false;
            loanTask.IsRunning = false;
            returnTask.IsRunning = false;
        }

        public void UpdateAllItems()
        {
            string[] loanItemStrings = loanItemManager.GetLoanItemInfoStrings();
            string[] productStrings = productManager.GetProductInfoStrings();
            string[] combined = new[] { "=== Loaned Items ===" }.Concat(loanItemStrings).Concat(new[] { "", "=== Available Products ===" }).Concat(productStrings).ToArray();

            Debug.WriteLine("Updating UI with " + productStrings.Length + " products");
            UpdateProductListBox(combined);
        }

        public void UpdateProductListBox(string[] productStrings)
        {
            if (productListBox.InvokeRequired)
            {
                productListBox.Invoke(new Action<string[]>(UpdateProductListBox), new Object[] { productStrings });
            }
            else
            {
                if (productListBox.Items.Count != productStrings.Length)
                {
                    productListBox.Items.Clear(); 
                }

                foreach (string item in productStrings)
                {
                    if (!productListBox.Items.Contains(item)) 
                    {
                        productListBox.Items.Add(item);
                    }
                }
            }
        }

        public void WriteOutput(string message)
        {
            if (productListBox.InvokeRequired)
            {
                productListBox.Invoke(new Action<string>(WriteOutput), message);
            }
            else
            {
                productListBox.Items.Add(message);
            }
        }

        public void UpdateEventListBox(string[] eventStrings)
        {
            if (eventLogListBox.InvokeRequired)
            {
                eventLogListBox.Invoke(new Action<string[]>(UpdateEventListBox), new Object[] { eventStrings });
            }
            else
            {
                eventLogListBox.Items.Clear(); 
                foreach (string item in eventStrings)
                {
                    if (!eventLogListBox.Items.Contains(item)) 
                    {
                        eventLogListBox.Items.Add(item);  
                    }
                }
            }
        }

        public void WriteEventLog(string message)
        {
            if (eventLogListBox.InvokeRequired) 
            {
                eventLogListBox.Invoke(new Action<string>(WriteEventLog), message);
            }
            else
            {
                eventLogListBox.Items.Add(message);  
            }
        }
    }
}
