﻿/*
    Copyright (C) 2014-2017 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.ComponentModel.Composition;
using dnSpy.Contracts.Debugger.DotNet.Code;
using dnSpy.Contracts.Metadata;

namespace dnSpy.Debugger.DotNet.Code {
	[Export(typeof(DbgDotNetCodeLocationFactory))]
	sealed class DbgDotNetCodeLocationFactoryImpl : DbgDotNetCodeLocationFactory {
		readonly BreakpointFormatterService breakpointFormatterService;

		[ImportingConstructor]
		DbgDotNetCodeLocationFactoryImpl(BreakpointFormatterService breakpointFormatterService) =>
			this.breakpointFormatterService = breakpointFormatterService;

		public override DbgDotNetCodeLocation Create(ModuleId module, uint token, uint offset) {
			var dnbp = new DbgDotNetCodeLocationImpl(this, module, token, offset);
			var formatter = breakpointFormatterService.Create(dnbp);
			dnbp.Formatter = formatter;
			return dnbp;
		}
	}
}