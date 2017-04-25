using System;
using web.ViewModels.User;

namespace web.ViewModels.Post
{
    public class PostModel
    {
        #region Constructors

        public PostModel()
        {
        }

        public PostModel(Data.Entities.Post post)
        {
            Caption = post.Caption;
            Id = post.Id;
            EnteredAt = post.EnteredAt;
            EnteredBy = new UserModel(post.EnteredBy);
            PostUrl = $"/post/{post.Id}";
            ImageUrl = $"/post/{post.Id}/image.jpg";
        }

        #endregion

        #region Properties

        public string Caption { get; set; }

        public DateTimeOffset EnteredAt { get; set; }

        public UserModel EnteredBy { get; set; }

        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string PostUrl { get; set; }

        #endregion
    }
}