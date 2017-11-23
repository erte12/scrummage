using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Scrummage.Core;
using Scrummage.Core.Services;
using Scrummage.Core.Services.Validation;
using Scrummage.Models;

namespace Scrummage.Services
{
    public class TeamsService : ITeamsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IValidationDictionary _validationDictionary;

        public TeamsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Initialize(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
        }

        public void Validate(Team team)
        {
            if (team.Name == null || team.Name.Trim().Length == 0)
                _validationDictionary.AddError("Name", "Name is required.");
            if (team.Name != null && team.Name.Trim().Length < 2)
                _validationDictionary.AddError("Content", "Name must contain at least 2 characters.");
        }

        public Team Create(Team newTeam)
        {
            newTeam.ScrumMasterId = HttpContext.Current.User.Identity.GetUserId();
            newTeam.CreatedAt = DateTime.Now;
            Validate(newTeam);

            if (_validationDictionary.IsValid)
            {
                _unitOfWork.Teams.Add(newTeam);
                _unitOfWork.Complate();
            }

            return newTeam;
        }

        public bool AddMember(int teamId, string newMemberId)
        {
            var team = _unitOfWork.Teams.Get(teamId);
            var newMember = _unitOfWork.Users.Get(newMemberId);

            if (team == null || newMember == null)
                return false;

            team.Users.Add(newMember);
            _unitOfWork.Complate();

            return true;
        }

        public bool RemoveMember(int teamId, string memberId)
        {
            var team = _unitOfWork.Teams.GetWithMembers(teamId);
            var member = _unitOfWork.Users.Get(memberId);

            if (team == null || member == null)
                return false;

            var result = team.Users.Remove(member);
            _unitOfWork.Complate();

            return result;
        }

        public bool DeleteTeam(int teamId)
        {
            var team = _unitOfWork.Teams.Get(teamId);

            if (team == null)
                return false;

            _unitOfWork.Teams.Remove(team);
            var result = _unitOfWork.Complate();

            return result > 0;
        }
    }
}