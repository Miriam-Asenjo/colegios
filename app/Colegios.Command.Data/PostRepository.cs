using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using Colegios.Command.Core.DataInterfaces;
using Colegios.Query.Core.DTOs;

namespace Colegios.Command.Data
{
    [SessionFactoryAttribute(Constants.QueryFactoryKey)]
    public class PostRepository : NHibernateRepository<PostReadDto>, IPostRepository
    {
        public void SaveOrUpdate (int postId){

            var postRead = base.Get(postId);

            if (postRead == null)
            {
                postRead = new PostReadDto();
                postRead.PostId = postId;
                postRead.Count = 1;
                postRead.Enabled = true;
                base.Save(postRead);
            }
            else
            {
                postRead.Count = postRead.Count + 1;
                base.Update(postRead);
            }
        }

    }
}
