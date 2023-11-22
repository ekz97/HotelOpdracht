using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    //public class MemberManager   =====> misschien overbodig , later zal blijken
    //{
    //    private IMemberRepository _memberRepository;
        
    //    public MemberManager(IMemberRepository memberRepository)
    //    {
    //        _memberRepository = memberRepository;
    //    }

    //    public IReadOnlyList<Member> GetMembers(string filter)
    //    {
    //        try
    //        {
    //            return _memberRepository.GetMembers(filter);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new CustomerManagerException("GetMembers");
    //        }
    //    }

    //    public Member GetMember(int? id)
    //    {
    //        try
    //        {
    //            return _memberRepository.GetMemberById(id);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new CustomerManagerException("AddMember", ex);
    //        }
    //    }

    //    public void AddMember(Member member)
    //    {
    //        try
    //        {
    //            _memberRepository.AddMember(member);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new CustomerManagerException("AddMember", ex);
    //        }
    //    }

    //    public void UpdateMember(Member member)
    //    {
    //        try
    //        {
    //            _memberRepository.UpdateMember(member);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new CustomerManagerException("UpdateMemner", ex);
    //        }
    //    }
    //}
}
