using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using AgendaConsole.Models;
using AgendaConsole.JsonStorage.Interfaces;

namespace AgendaConsole.JsonStorage
{
    public class JsonStorage<T> : IJsonStorage<T> where T : Register
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly List<T> _context;
        public IEnumerable<T> Context => _context;
        public JsonStorage()
        {
            _filePath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "default_store.json";
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            _context = ReadFile().ToList();
        }

        public T Create(T model)
        {
            try
            {
                var id = _context.Any() ? _context.LastOrDefault().Id + 1 : _context.Count() + 1;
                model.Id = id;
                model.CreatedAt = DateTime.Now;

                _context.Add(model);

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _context;
        }

        public T GetById(int id)
        {
            return _context.FirstOrDefault(x => x.Id == id);
        }


        public T Remove(int id)
        {
            try
            {
                var model = GetById(id);
                if (model is null)
                    throw new Exception($"${typeof(T)} Not found");

                if (!_context.Remove(model))
                    throw new Exception($"Error removing ${typeof(T)}");

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                if (!FileExists())
                    File.Create(_filePath).Close();

                await File.WriteAllTextAsync(
                    _filePath,
                    JsonSerializer.Serialize(_context, _jsonSerializerOptions),
                    Encoding.UTF8
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public T Update(T model)
        {
            try
            {
                var result = GetById(model.Id);
                if (result is null)
                    throw new Exception($"${typeof(T)} Not found");

                model.CreatedAt = result.CreatedAt;
                model.UpdatedAt = DateTime.Now;

                _context[_context.IndexOf(result)] = model;

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IEnumerable<T> ReadFile()
        {
            if (!FileExists())
                return new List<T>();

            var result = File.ReadAllText(_filePath);

            if (string.IsNullOrEmpty(result))
                return new List<T>();

            try
            {
                return JsonSerializer.Deserialize<IEnumerable<T>>(result);
            }
            catch (JsonException ex)
            {
                throw new FileLoadException($"Error starting file: ${ex}");
            }
        }

        private bool FileExists()
        {
            return File.Exists(_filePath);
        }
    }
}
