using FluentValidation;
using web.ViewModels.Post;

namespace web.Validators.Post
{
    public class PostCreateModelValidator : AbstractValidator<PostCreateModel>
    {
        #region Constructors

        public PostCreateModelValidator()
        {
            RuleFor(x => x.Caption).Length(1, 160);
            RuleFor(x => x.File).NotNull();
        }

        #endregion
    }
}