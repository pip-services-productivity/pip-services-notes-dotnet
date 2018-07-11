using System;
using Interface.Data.Version1;
using Interface.Logic;
using PipServices.Commons.Commands;
using PipServices.Commons.Convert;
using PipServices.Commons.Data;
using PipServices.Commons.Validate;
using TypeCode = PipServices.Commons.Convert.TypeCode;

namespace Service.Logic
{
 public class NotesCommandSet : CommandSet
    {
        private INotesController _controller;

        public NotesCommandSet(INotesController controller) 
	    {
            _controller = controller;

            AddCommand(MakeCreateNoteCommand());
            AddCommand(MakeUpdateNoteCommand());
            AddCommand(MakeDeleteNoteByIdCommand());
            AddCommand(MakeGetNoteByIdCommand());
            AddCommand(MakeGetNotesCommand());
        }

        private ICommand MakeCreateNoteCommand()
        {
            return new Command(
                "create_note",
                new ObjectSchema()
                    .WithOptionalProperty("note", new NoteV1Schema()),
                async (correlationId, parameters) =>
                {
                    var note = ConvertToNotes(parameters.Get("note"));
                    return await _controller.CreateNoteAsync(correlationId, note);
                });
        }

        private ICommand MakeUpdateNoteCommand()
        {
            return new Command(
                "update_note",
                new ObjectSchema()
                    .WithOptionalProperty("note", new NoteV1Schema()),
                async (correlationId, parameters) =>
                {
                    var note = ConvertToNotes(parameters.Get("note"));
                    return await _controller.UpdateNoteAsync(correlationId, note);
                });
        }

        private ICommand MakeDeleteNoteByIdCommand()
        {
            return new Command(
                "delete_note_by_id",
                new ObjectSchema()
                    .WithRequiredProperty("id", TypeCode.String),
                async (correlationId, parameters) =>
                {
                    var id = parameters.GetAsString("id");
                    return await _controller.DeleteNoteByIdAsync(correlationId, id);
                });
        }

        private ICommand MakeGetNoteByIdCommand()
        {
            return new Command(
                "get_note_by_id",
                new ObjectSchema()
                    .WithRequiredProperty("id", TypeCode.String),
                async (correlationId, parameters) =>
                {
                    var id = parameters.GetAsString("id");
                    Console.WriteLine("------------" + id);
                    return await _controller.GetNoteByIdAsync(correlationId, id);
                });
        }

        private ICommand MakeGetNotesCommand()
        {
            return new Command(
                "get_notes",
                new ObjectSchema()
                    .WithOptionalProperty("filter", new FilterParamsSchema())
                    .WithOptionalProperty("paging", new PagingParamsSchema()),
                async (correlationId, parameters) =>
                {
                    var filter = FilterParams.FromValue(parameters.Get("filter"));
                    var paging = PagingParams.FromValue(parameters.Get("paging"));
                    return await _controller.GetNotesAsync(correlationId, filter, paging);
                });
        }

        private static NoteV1 ConvertToNotes(object value)
        {
            return JsonConverter.FromJson<NoteV1>(JsonConverter.ToJson(value));
        }
    }   
}