﻿// <copyright file="Container.cs" company="Windower Team">
// Copyright © 2013-2014 Windower Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
// </copyright>

namespace ResourceExtractor.Serializers.Lua
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class LuaFile
    {
        private string Name { get; set; }
        private List<LuaElement> Elements { get; set; }
        public LuaFile(string Name)
        {
            this.Name = Name;
            Elements = new List<LuaElement>();
        }

        public void Add(dynamic e)
        {
            Elements.Add(new LuaElement(e));
        }

        public void Save()
        {
            using (StreamWriter file = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "resources", "lua", String.Format("{0}.lua", Name))))
            {
                file.WriteLine("-- Automatically generated file: {0}", Name.First().ToString().ToUpper() + String.Join("", Name.Skip(1)));
                file.WriteLine();
                file.WriteLine("local {0} = {{", Name);

                foreach (var e in from e in Elements orderby e.ID select e)
                {
                    file.WriteLine("    [{0}] = {1},", e.ID, e.ToString());
                }

                file.WriteLine("}");
                file.WriteLine();
                file.WriteLine("return {0}", Name);
                file.WriteLine();
                file.WriteLine("--[[");
                file.WriteLine("Copyright © 2013-{0}, Windower", DateTime.Now.Year);
                file.WriteLine("All rights reserved.");
                file.WriteLine();
                file.WriteLine("Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:");
                file.WriteLine();
                file.WriteLine("    * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.");
                file.WriteLine("    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.");
                file.WriteLine("    * Neither the name of Windower nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.");
                file.WriteLine();
                file.WriteLine("THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS \"AS IS\" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL Windower BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.");
                file.WriteLine("]]");
            }
        }
    }
}
