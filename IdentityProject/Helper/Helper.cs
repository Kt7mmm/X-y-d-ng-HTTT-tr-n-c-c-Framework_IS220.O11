using IdentityProject.Context;
using IdentityProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace Cinema.Helper
{
    public class Helper
    {
        private readonly CinemaDbContext _context;
        public Helper(CinemaDbContext context) 
        {
            _context = context;
        }
        public string setseat(string sl_id)
        {

            string html = "";
                int count = 0;

            string room = _context.Slots.Where(s => s.sl_id == sl_id).Select(s => s.r_id).First();
        
            count = 120;
            html += "<div class=\"row\">";
            for (int i = 1; i< 13; i++){
                string tkid = "A" + i.ToString();
                Ticket ticket = null;

                if (_context.Tickets.Where(s => s.sl_id == sl_id).Where(s => s.st_id == tkid).Where(s => s.r_id == room).Any())
                {
                    ticket = _context.Tickets.Where(s => s.sl_id == sl_id).Where(s => s.st_id == tkid).Where(s => s.r_id == room).First();
                }

                if (ticket == null){
                    html += "<label style = \"cursor: pointer;\" >";
                    html += "<div class=\"standard\" style=\"top: 252px; left: " + count.ToString() + "px; position: absolute; \">";
                    html += "<input type = \"checkbox\" name=\"tickets[]\" id=\"" + tkid + "\" value=\"" + tkid + "\" onclick=\"myFunction(" + tkid + ")\">" + tkid;
                    html += "</div>"; 
                }
                else {
                    html += "<label>";
                    html += "<div class=\"standard\" style=\"top: 252px; left: " + count.ToString() + "px; position: absolute; background-color: #D80032\">" + tkid + "</div>";
                }
                    count += 45;
            }
            html += "</div>";

            count = 120;
            html += "<div class=\"row\">";
            for (int i = 1; i < 13; i++)
            {
                string tkid = "B" + i.ToString();
                Ticket ticket = null;

                if (_context.Tickets.Where(s => s.sl_id == sl_id).Where(s => s.st_id == tkid).Where(s => s.r_id == room).Any())
                {
                    ticket = _context.Tickets.Where(s => s.sl_id == sl_id).Where(s => s.st_id == tkid).Where(s => s.r_id == room).First();
                }

                if (ticket == null)
                {
                    html += "<label style = \"cursor: pointer;\" >";
                    html += "<div class=\"standard\" style=\"top: 302px; left: " + count.ToString() + "px; position: absolute; \">";
                    html += "<input type = \"checkbox\" name=\"tickets[]\" id=\"" + tkid + "\" value=\"" + tkid + "\" onclick=\"myFunction(" + tkid + ")\">" + tkid;
                    html += "</div>";
                }
                else
                {
                    html += "<label>";
                    html += "<div class=\"standard\" style=\"top: 302px; left: " + count.ToString() + "px; position: absolute; background-color: #D80032\">" + tkid + "</div>";
                }
                count += 45;
            }
            html += "</div>";

            count = 120;
            html += "<div class=\"row\">";
            for (int i = 1; i < 13; i++)
            {
                string tkid = "C" + i.ToString();
                Ticket ticket = null;

                if (_context.Tickets.Where(s => s.sl_id == sl_id).Where(s => s.st_id == tkid).Where(s => s.r_id == room).Any())
                {
                    ticket = _context.Tickets.Where(s => s.sl_id == sl_id).Where(s => s.st_id == tkid).Where(s => s.r_id == room).First();
                }

                if (ticket == null)
                {
                    html += "<label style = \"cursor: pointer;\" >";
                    html += "<div class=\"standard\" style=\"top: 352px; left: " + count.ToString() + "px; position: absolute; \">";
                    html += "<input type = \"checkbox\" name=\"tickets[]\" id=\"" + tkid + "\" value=\"" + tkid + "\" onclick=\"myFunction(" + tkid + ")\">" + tkid;
                    html += "</div>";
                }
                else
                {
                    html += "<label>";
                    html += "<div class=\"standard\" style=\"top: 352px; left: " + count.ToString() + "px; position: absolute; background-color: #D80032\">" + tkid + "</div>";
                }
                count += 45;
            }
            html += "</div>";


            count = 120;
            html += "<div class=\"row\">";
            for (int i = 1; i < 13; i++)
            {
                string tkid = "D" + i.ToString();
                Ticket ticket = null;

                if (_context.Tickets.Where(s => s.sl_id == sl_id).Where(s => s.st_id == tkid).Where(s => s.r_id == room).Any())
                {
                    ticket = _context.Tickets.Where(s => s.sl_id == sl_id).Where(s => s.st_id == tkid).Where(s => s.r_id == room).First();
                }

                if (ticket == null)
                {
                    html += "<label style = \"cursor: pointer;\" >";
                    html += "<div class=\"vip\" style=\"top: 402px; left: " + count.ToString() + "px; position: absolute; \">";
                    html += "<input type = \"checkbox\" name=\"tickets[]\" id=\"" + tkid + "\" value=\"" + tkid + "\" onclick=\"myFunction(" + tkid + ")\">" + tkid;
                    html += "</div>";
                }
                else
                {
                    html += "<label>";
                    html += "<div class=\"vip\" style=\"top: 402px; left: " + count.ToString() + "px; position: absolute; background-color: #D80032\">" + tkid + "</div>";
                }
                count += 45;
            }
            html += "</div>";


            count = 120;
            html += "<div class=\"row\">";
            for (int i = 1; i < 13; i++)
            {
                string tkid = "E" + i.ToString();
                Ticket ticket = null;

                if (_context.Tickets.Where(s => s.sl_id == sl_id).Where(s => s.st_id == tkid).Where(s => s.r_id == room).Any())
                {
                    ticket = _context.Tickets.Where(s => s.sl_id == sl_id).Where(s => s.st_id == tkid).Where(s => s.r_id == room).First();
                }

                if (ticket == null)
                {
                    html += "<label style = \"cursor: pointer;\" >";
                    html += "<div class=\"vip\" style=\"top: 452px; left: " + count.ToString() + "px; position: absolute; \">";
                    html += "<input type = \"checkbox\" name=\"tickets[]\" id=\"" + tkid + "\" value=\"" + tkid + "\" onclick=\"myFunction(" + tkid + ")\">" + tkid;
                    html += "</div>";
                }
                else
                {
                    html += "<label>";
                    html += "<div class=\"vip\" style=\"top: 452px; left: " + count.ToString() + "px; position: absolute; background-color: #D80032\">" + tkid + "</div>";
                }
                count += 45;
            }
            html += "</div>";


            count = 120;
            html += "<div class=\"row\">";
            for (int i = 1; i < 13; i++)
            {
                string tkid = "F" + i.ToString();
                Ticket ticket = null;

                if (_context.Tickets.Where(s => s.sl_id == sl_id).Where(s => s.st_id == tkid).Where(s => s.r_id == room).Any())
                {
                    ticket = _context.Tickets.Where(s => s.sl_id == sl_id).Where(s => s.st_id == tkid).Where(s => s.r_id == room).First();
                }

                if (ticket == null)
                {
                    html += "<label style = \"cursor: pointer;\" >";
                    html += "<div class=\"vip\" style=\"top: 502px; left: " + count.ToString() + "px; position: absolute; \">";
                    html += "<input type = \"checkbox\" name=\"tickets[]\" id=\"" + tkid + "\" value=\"" + tkid + "\" onclick=\"myFunction(" + tkid + ")\">" + tkid;
                    html += "</div>";
                }
                else
                {
                    html += "<label>";
                    html += "<div class=\"vip\" style=\"top: 502px; left: " + count.ToString() + "px; position: absolute; background-color: #D80032\">" + tkid + "</div>";
                }
                count += 45;
            }
            html += "</div>";


            count = 150;
            html += "<div class=\"row\">";
            for (int i = 1; i < 7; i++)
            {
                string tkid = "G" + i.ToString();
                Ticket ticket = null;

                if (_context.Tickets.Where(s => s.sl_id == sl_id).Where(s => s.st_id == tkid).Where(s => s.r_id == room).Any())
                {
                    ticket = _context.Tickets.Where(s => s.sl_id == sl_id).Where(s => s.st_id == tkid).Where(s => s.r_id == room).First();
                }

                if (ticket == null)
                {
                    html += "<label style = \"cursor: pointer;\" >";
                    html += "<div class=\"sweetbox\" style=\"top: 552px; left: " + count.ToString() + "px; position: absolute; \">";
                    html += "<input type = \"checkbox\" name=\"tickets[]\" id=\"" + tkid + "\" value=\"" + tkid + "\" onclick=\"myFunction(" + tkid + ")\">" + tkid;
                    html += "</div>";
                }
                else
                {
                    html += "<label>";
                    html += "<div class=\"sweetbox\" style=\"top: 552px; left: " + count.ToString() + "px; position: absolute; background-color: #D80032\">" + tkid + "</div>";
                }
                count += 80;
            }
            html += "</div>";

            return html;
        }
    }
}
