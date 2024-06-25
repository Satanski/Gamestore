using AutoMapper;
using Gamestore.BLL.Models;
using Gamestore.DAL.Entities;

namespace Gamestore.BLL.Helpers;

internal class CommentHelpers(IMapper automapper)
{
    private const string QuoteTemplateString = "[$Quote$]";

    internal List<CommentModel> CommentListCreator(List<Comment> comments)
    {
        List<CommentModel> commentList = [];
        foreach (var comment in comments)
        {
            if (comment.ParentCommentId != null)
            {
                FindParentComment(comment, commentList, comment.ParentCommentId);
            }
            else
            {
                commentList.Add(automapper.Map<CommentModel>(comment));
            }
        }

        return commentList;
    }

    private bool FindParentComment(Comment comment, List<CommentModel> commentList, Guid? parentCommentId)
    {
        var parentComment = commentList.Find(x => x.Id == parentCommentId);
        if (parentComment != null)
        {
            var commentModel = ComposeComment(automapper, comment, parentComment);
            parentComment.ChildComments.Add(commentModel);
            return true;
        }
        else
        {
            foreach (var com in (from com in commentList
                                 from c in com.ChildComments
                                 select com).Distinct())
            {
                var isFound = FindParentComment(comment, com.ChildComments, parentCommentId);
                if (isFound)
                {
                    break;
                }
            }
        }

        return false;
    }

    private static CommentModel ComposeComment(IMapper automapper, Comment comment, CommentModel? parentComment)
    {
        var commentModel = automapper.Map<CommentModel>(comment);

        if (commentModel.Body.Contains(QuoteTemplateString, StringComparison.InvariantCultureIgnoreCase))
        {
            commentModel.Body = commentModel.Body.Replace(QuoteTemplateString, string.Empty);
            commentModel.Body = commentModel.Body.Insert(0, $"[Reply to: {parentComment.Name} \"{parentComment.Body}\"] ");
        }
        else
        {
            commentModel.Body = commentModel.Body.Insert(0, $"[Reply to: {parentComment.Name}] ");
        }

        return commentModel;
    }
}
