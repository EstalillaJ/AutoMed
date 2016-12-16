using AutoMed.Models;

namespace AutoMed.DAL
{
    public interface IQuoteRepository
    {
        void Approve(Quote quote);
        void Create(Quote quote);
    }
}