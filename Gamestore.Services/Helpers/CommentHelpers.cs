using AutoMapper;
using Gamestore.BLL.Models;
using Gamestore.DAL.Entities;

namespace Gamestore.BLL.Helpers;

internal class CommentHelpers(IMapper automapper)
{
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
            parentComment.ChildComments.Add(automapper.Map<CommentModel>(comment));
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
}
