using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using Colegios.Query.Core.QueryInterfaces;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query
{
    public class PostQuery: NHibernateQuery, IPostQuery
    {
        public IEnumerable<PostReadDto> GetTopPostRead(int numPosts)
        {
            var topPosts = Session.QueryOver<PostReadDto>().Where(x => x.Enabled).OrderBy(x => x.Count).Desc.Take(numPosts).List();
            return topPosts;

        }
    }
}
