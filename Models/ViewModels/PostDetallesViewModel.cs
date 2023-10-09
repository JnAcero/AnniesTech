
namespace AnniesTech.Models.ViewModels
{
    public class PostDetallesViewModel
    {
        public Post? Post { get; set; }
        public List<Post>? ComentariosPrincipales { get; set; }
        public List<Comentario>? ComentariosHijos { get; set; }
        public List<Comentario> ?ComentariosNietos { get; set; }
        public List<Post>? PostsRecientes { get; set; }
    }
}