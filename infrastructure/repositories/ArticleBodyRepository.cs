﻿using domain.entities;
using domain.interfaces;

namespace infrastructure.repositories;

public class ArticleBodyRepository : IArticleBodyRepository
{
    private readonly AppDbContext _context;

    public ArticleBodyRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddArticleBody(ArticleBody articleBody)
    {
        _context.ArticleBodies.Add(articleBody);
    }
}