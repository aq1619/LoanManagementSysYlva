using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSys.Managers
{
    internal class LoanItemManager
    {

        private List<LoanItem> loanItems = new();
        private LoanSystem loanSystem;
        public void Add(LoanItem loanItem)
        {
            loanItems.Add(loanItem);
            //loanSystem.UpdateAllItems();
        }

        public void Remove(int index)
        {
            if (CheckIndex(index))
                loanItems.RemoveAt(index);
        }

        public int Count => loanItems.Count;

        public LoanItem Get(int index)
        {
            if (CheckIndex(index))
                return loanItems[index];
            else
                return null;
        }
        private bool CheckIndex(int index)
        {
            return (index >= 0) && (index < loanItems.Count);
        }

        public string[] GetLoanItemInfoStrings()
        {
            if (loanItems.Count == 0)
                return new string[] { "No loaned items." };

            string[] infoStrings = new string[loanItems.Count + 2];
            infoStrings[0] = $"Number of loaned items: {loanItems.Count}";
            infoStrings[1] = "";

            for (int i = 0; i < loanItems.Count; i++)
                infoStrings[i + 2] = loanItems[i].ToString();

            return infoStrings;
        }
    }
}
