﻿using Elibri.EF.Models;
using Microsoft.EntityFrameworkCore;
using Elibri.Core.Repository.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Core.Repository.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository(Context context)
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.UserName == username);
        }
        public async Task<User> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<User> CreateAsync(User entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(User entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}
