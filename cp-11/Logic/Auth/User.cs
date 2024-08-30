﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using cp_11.Logic.model;

namespace cp_11.Logic.Auth
{
    public class User : IEquatable<User>
    {
        public List<Ticket> Tickets { get; set; }
        public string FirstName { get; set; }
        public string Hash { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }

        public bool Equals(User? other)
        {
            if(FirstName == other.FirstName && LastName == other.LastName) return true;
            return false;
        }
    }

    public class Admin : User, IEquatable<Admin>
    {
        public bool IsAdmin = true;

        public bool Equals(Admin? other)
        {
            throw new NotImplementedException();
        }
    }

}
