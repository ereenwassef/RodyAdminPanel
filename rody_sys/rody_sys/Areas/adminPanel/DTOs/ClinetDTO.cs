using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rody_sys.Areas.adminPanel.DTOs
{
    public class ClinetDTO
    {
        public int id { get; set; }
        public double? charge { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string contract_date { get; set; }
        public string activation_date { get; set; }
        public string govern { get; set; }
        public int? governId { get; set; }
        public string areaName { get; set; }
        public int? areaId { get; set; }
        public string stay_place_get { get; set; }
        public int? num_followers { get; set; }
        public int? parentId { get; set; }

        public string jop { get; set; }
        public string date_of_birth { get; set; }
        public string id_num { get; set; }
        public string id_place { get; set; }
        public string work_name { get; set; }
        public string work_place { get; set; }
        public string stay_place { get; set; }
        public int? stay_place_floor { get; set; }
        public int? stay_place_flat { get; set; }
        public int? delegatorId { get; set; }
        public int? agentId { get; set; }
        public int? available { get; set; }
        public string notes { get; set; }
        public double? MsgCharge { get; set; }
        public List<PhoneDTO> phones { get; set; }
    }
    public class PhoneDTO
    {
        public string name { get; set; }
        public string number { get; set; }
    }
}