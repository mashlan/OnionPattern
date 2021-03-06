﻿using OnionPattern.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace OnionPattern.Domain.Entities
{
    public class Platform : VideoGameEntity, IPlatform
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }

        public ICollection<GamePlatform> GamePlatforms { get; set; }
    }
}
