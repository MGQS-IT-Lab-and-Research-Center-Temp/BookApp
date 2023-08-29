﻿using BookApp.Entities;
using System.Linq.Expressions;

namespace BookApp.Repository.Interfaces;

public interface ICommentRepository : IRepository<Comment>
{
    Task<Comment> GetCommentWithReportList(string id);
    Task<Comment> GetCommentWithReportList(Expression<Func<Comment, bool>> expression);
    Task<IList<CommentReport>> GetCommentReportsByCommentId(string id);
}
