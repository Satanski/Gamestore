﻿using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class CategoryRepository(IMongoDatabase database) : ICategoryRepository
{
    private const string CollectionName = "categories";

    private readonly IMongoCollection<MongoCategory> _collection = database.GetCollection<MongoCategory>(CollectionName);

    public Task<List<MongoCategory>> GetAllAsync()
    {
        var category = _collection.Find(_ => true).ToListAsync();
        return category;
    }

    public Task<MongoCategory> GetById(int id)
    {
        var category = _collection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
        return category;
    }
}
