using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSys.Managers
{
    internal class MemberManager
    {
        private List<Member> members = new();
        private int lastMemeberID = 0;

        public Member Get(int index)
        {
            if (CheckIndex(index))
                return members[index];
            else
                return null;
        }
        public List<Member> GetAllMembers()
        {
            return members;
        }
        public int Count => members.Count;

        private bool CheckIndex(int index)
        {
            return (index >= 0) && (index < members.Count);
        }

        public void AddMembers()
        {
            for (int i = 0; i < 25; i++)
            {
                AddNewMember(lastMemeberID.ToString());
            }
        }

        public Member AddNewMember(string name)
        {
            int id = lastMemeberID; 
            lastMemeberID++;  

            Member member = new Member(id, name);  
            members.Add(member);

            return member;
        }
        //public string[] GetMemberInfoStrings()
        //{
        //    if (members.Count == 0)
        //        return new string[] { "No members available." };

        //    string[] infoStrings = new string[members.Count + 2];
        //    infoStrings[0] = $"Number of members: {members.Count}";
        //    infoStrings[1] = "";

        //    for (int i = 0; i < members.Count; i++)
        //        infoStrings[i + 2] = members[i].ToString();

        //    return infoStrings;
        //}
    }
}
