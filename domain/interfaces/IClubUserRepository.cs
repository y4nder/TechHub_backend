﻿using domain.entities;

namespace domain.interfaces;

public interface IClubUserRepository
{
    void AddClubUser(ClubUser clubUser);
    Task AddClubUserRange(List<ClubUser> clubUsers);
}