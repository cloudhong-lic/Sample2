using System;

namespace Sample2.Domain.Models
{
	public class Farmer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTimeOffset UpdateTime { get; set; }
	}
}