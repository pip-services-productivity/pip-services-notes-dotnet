using Interface.Data.Version1;
using System;
using Xunit;

namespace Service.Test.Data
{
    public static class NoteTestModelV1
    {
	    public static NoteV1 GenerateRandomNote()
	    {
	        return new NoteV1
	        {
	            Title = Guid.NewGuid().ToString("N").Substring(0, 8),
	            Description = Guid.NewGuid().ToString("N").Substring(0, 16)
	        };
	    }

        public static void AssertEqual(NoteV1 expected, NoteV1 actual)
        {
            //Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Description, actual.Description);
        }
    }
}