using HashidsNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.HashId
{
    public class HashIdService
    {
        private readonly IHashids _hashids;

        public HashIdService(IHashids hashids)
        {
            _hashids = hashids;
        }

        public string Encode(int id)
        {
            return _hashids.Encode(id);
        }

        public int Decode(string hash)
        {
            var numbers = _hashids.Decode(hash);
            return numbers.Length > 0 ? numbers[0] : throw new Exception("Invalid hash");
        }
    }
}
