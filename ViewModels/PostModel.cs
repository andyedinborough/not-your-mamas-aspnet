using System;
using web.Data.Entities;

namespace web.ViewModels
{
    public class PostModel
    {
        #region Constructors

        public PostModel()
        {
        }

        public PostModel(Post post)
        {
            Caption = post.Caption;
            Id = post.Id;
            EnteredAt = post.EnteredAt;
            EnteredBy = new UserModel(post.EnteredBy);
            PostUrl = new Uri($"/post/{post.Id}");
            ImageUrl = new Uri($"/post/{post.Id}/image.jpg");
        }

        #endregion

        #region Properties

        public string Caption { get; set; }

        public DateTimeOffset EnteredAt { get; set; }

        public UserModel EnteredBy { get; set; }

        public int Id { get; set; }

        public Uri ImageUrl { get; set; }

        public Uri PostUrl { get; set; }

        #endregion
    }
}