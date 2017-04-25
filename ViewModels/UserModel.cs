﻿using web.Data.Entities;

namespace web.ViewModels
{
    public class UserModel
    {
        #region Constructors

        public UserModel()
        {
        }

        public UserModel(User user)
        {
            Name = user.Name;
            Email = user.Email;
            Id = user.Id;
            Username = user.Username;
        }

        #endregion

        #region Properties

        public string Email { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Username { get;  set; }

        #endregion
    }
}