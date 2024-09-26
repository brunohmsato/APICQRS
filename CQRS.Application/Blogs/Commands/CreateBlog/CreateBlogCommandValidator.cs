using FluentValidation;

namespace CQRS.Application.Blogs.Commands.CreateBlog;

public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
{
    public CreateBlogCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("O nome é obrigatório")
            .MaximumLength(200).WithMessage("O campo não deve exceder 200 caracteres.");

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("A descrição é obrigatória");

        RuleFor(v => v.Author)
           .NotEmpty().WithMessage("O autor é obrigatório")
           .MaximumLength(20).WithMessage("O campo não deve exceder 20 caracteres.");
    }
}
