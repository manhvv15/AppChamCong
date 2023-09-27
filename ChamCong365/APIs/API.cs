using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ChamCong365.APIs
{
    public class API
    {
        public const string img_source_api = "https://chamcong.24hpay.vn/upload/employee/";
        public const string company_infor_api = "http://210.245.108.202:3000/api/qlc/company/info";

        public const string list_shift_api = "http://210.245.108.202:3000/api/qlc/shift/list";
        public const string create_shift_api = "http://210.245.108.202:3000/api/qlc/shift/create";
        public const string detail_shift_api = "http://210.245.108.202:3000/api/qlc/shift/detail";
        public const string edit_shift_api = "http://210.245.108.202:3000/api/qlc/shift/edit";
        public const string delete_shift_api = "http://210.245.108.202:3000/api/qlc/shift/delete";

        public const string list_ChildCompany_api = "http://210.245.108.202:3000/api/qlc/company/child/list";
        public const string create_child_Company_api = "http://210.245.108.202:3000/api/qlc/company/child/create";
        public const string edit_child_company_api = "http://210.245.108.202:3000/api/qlc/company/child/edit";

        public const string list_department_api = "http://210.245.108.202:3000/api/qlc/department/list";
        public const string create_department_api = "http://210.245.108.202:3000/api/qlc/department/create";
        public const string edit_department_api = "http://210.245.108.202:3000/api/qlc/department/edit";
        public const string delete_department_api = "http://210.245.108.202:3000/api/qlc/department/del";

        public const string team_list_api = "http://210.245.108.202:3000/api/qlc/team/list";
        public const string create_team_api = "http://210.245.108.202:3000/api/qlc/team/create";
        public const string edit_team_api = "http://210.245.108.202:3000/api/qlc/team/edit";
        public const string delete_team_api = "http://210.245.108.202:3000/api/qlc/team/del";

        public const string group_listAll_api = "http://210.245.108.202:3000/api/qlc/group/listAll";
        public const string search_group_api = "http://210.245.108.202:3000/api/qlc/group/search";
        public const string create_group_api = "http://210.245.108.202:3000/api/qlc/group/create";
        public const string edit_group_api = "http://210.245.108.202:3000/api/qlc/group/edit";
        public const string delete_group_api = "http://210.245.108.202:3000/api/qlc/group/del";

        public const string list_TranferJob_api = "http://210.245.108.202:3006/api/hr/personalChange/getListTranferJob";
        public const string create_TranferJob_api = "http://210.245.108.202:3006/api/hr/personalChange/updateTranferJob";
        public const string edit_TranferJob_api = "http://210.245.108.202:3006/api/hr/personalChange/updateTranferJob";
        public const string delete_TranferJob_api = "http://210.245.108.202:3006/api/hr/personalChange/deleteTranferJob";

        public const string list_QuitJobNew_api = "http://210.245.108.202:3006/api/hr/personalChange/getListQuitJobNew";
        public const string create_QuitJobNew_api = "http://210.245.108.202:3006/api/hr/personalChange/updateQuitJobNew";
        public const string update_QuitJobNew_api = "http://210.245.108.202:3006/api/hr/personalChange/updateQuitJobNew";
        public const string delete_QuitJobNew_api = "http://210.245.108.202:3006/api/hr/personalChange/deleteQuitJobNew";


        public const string list_QuitJob_api = "http://210.245.108.202:3006/api/hr/personalChange/getListQuitJob";
        public const string create_QuitJob_api = "http://210.245.108.202:3006/api/hr/personalChange/updateQuitJob";
        public const string update_QuitJob_api = "http://210.245.108.202:3006/api/hr/personalChange/updateQuitJob";
        public const string delete_QuitJob_api = "http://210.245.108.202:3006/api/hr/personalChange/deleteQuitJob";

        public const string managerUser_list_api = "http://210.245.108.202:3000/api/qlc/managerUser/list";
        public const string managerUser_all = "http://210.245.108.202:3000/api/qlc/managerUser/listAll";
        public const string list_position_api = "http://210.245.108.202:3006/api/hr/organizationalStructure/listPosition";
        public const string edit_employee_api = "http://210.245.108.202:3000/api/qlc/managerUser/edit";
        public const string managerUser_del_api = "http://210.245.108.202:3000/api/qlc/managerUser/del";
        public const string managerUser_create_api = "http://210.245.108.202:3000/api/qlc/managerUser/create";
        //lấy tất cả quy định mặc đinh chỉ có 10000 quy định
        public const string provision_list_api = "http://210.245.108.202:3006/api/hr/administration/listProvision?pageSize=10000&page=1";

        public const string RecruitmentNew_list_api = "http://210.245.108.202:3006/api/hr/recruitment/listNews";
        public const string setting_permision = "http://210.245.108.202:3006/api/hr/setting/permision";
        public const string create_process_api = "http://210.245.108.202:3006/api/hr/recruitment/createProcess";
        public const string organizationalStructure_detailInfoCompany = "http://210.245.108.202:3006/api/hr/organizationalStructure/detailInfoCompany";


        public const string edit_description_api = "http://210.245.108.202:3006/api/hr/organizationalStructure/updateDescription";
        public const string organizationalStructure_description = "http://210.245.108.202:3006/api/hr/organizationalStructure/description";
        public const string organizationalStructure_listEp = "http://210.245.108.202:3006/api/hr/organizationalStructure/listEmUntimed";

        public const string vanthu_setting_api= "http://210.245.108.202:3005/api/vanthu/setting/editSetting";
        public const string vanthu_setting_getData_api = "http://210.245.108.202:3005/api/vanthu/setting/createF";

        #region HR
        public const string login_employee_api = "https://chamcong.24hpay.vn/service/login_employee.php";
        public const string login_company_api = "https://chamcong.24hpay.vn/service/login_company.php"; //api đăng nhập công ty
        public const string send_otp_password_api = "https://chamcong.24hpay.vn/service/send_otp.php"; //api lấy mã xác nhận
        public const string forgot_password_employee_api = "https://chamcong.24hpay.vn/service/forget_pass_employee.php"; //api đặt lại mật khẩu nhân viên
        public const string forgot_password_company_api = "https://chamcong.24hpay.vn/service/forget_pass_company.php"; //api đặt lại mật khẩu công ty
        public const string verify_otp = "https://chamcong.24hpay.vn/service/verify_otp.php";

        // api trang chủ
        public const string avatar_uri = "https://chamcong.24hpay.vn/upload/employee/";
        public const string logo_uri = "https://chamcong.24hpay.vn/upload/company/logo/";
        public const string total_employee_api = "https://chamcong.24hpay.vn/service/get_count_employee.php?id_com=";
        public const string achievements_total_per_month = "https://phanmemnhansu.timviec365.vn/totalAchievement";
        public const string violation_total_per_month = "https://phanmemnhansu.timviec365.vn/totalInfringe";
        public const string candidate_total = "https://phanmemnhansu.timviec365.vn/totalCandidate";
        public const string interview_total = "https://phanmemnhansu.timviec365.vn/totalInterview";
        public const string offerjob_total = "https://phanmemnhansu.timviec365.vn/totalOfferJob";
        public const string weather_api = "https://phanmemnhansu.timviec365.vn/apiWeather";

        // api quản lý tuyển dụng
        // api quy trình tuyển dụng
        public const string list_recuitment = "https://phanmemnhansu.timviec365.vn/listRecuitment";
        public const string list_recruitment_stage = "https://phanmemnhansu.timviec365.vn/listRecruitmentStage";
        public const string delete_recuitment = "https://phanmemnhansu.timviec365.vn/deleteRecuitment"; // xóa quy trình tuyển dụng
        public const string add_recruitment = "https://phanmemnhansu.timviec365.vn/addRecuitment"; // thêm mới quy trình tuyển dụng
        public const string delete_stage = "https://phanmemnhansu.timviec365.vn/deleteRecuitmentStage"; // xóa giai đoạn tuyển dụng
        public const string edit_recruitment = "https://phanmemnhansu.timviec365.vn/editRecuitment"; // chỉnh sửa quy trình
        public const string edit_stage = "https://phanmemnhansu.timviec365.vn/editRecuitmentStage"; // chỉnh sửa giai đoạn tuyển dụng
        public const string add_stage = "https://phanmemnhansu.timviec365.vn/addRecuitmentStage"; // thêm mới giai đoạn tuyển dụng

        // api thực hiện tuyển dụng
        public const string total_candidate_by = "https://phanmemnhansu.timviec365.vn/totalCandidateBy";
        public const string lits_new_active = "https://phanmemnhansu.timviec365.vn/listNewActive";
        public const string list_schedule = "https://phanmemnhansu.timviec365.vn/listSchedule";
        public const string list_news = "https://phanmemnhansu.timviec365.vn/listNews";
        public const string perform_detail = "https://phanmemnhansu.timviec365.vn/performDetail";
        public const string removeRcruitmentNew = "https://phanmemnhansu.timviec365.vn/removeRecruitmentNew"; // gỡ tin tuyển dụng
        public const string setSampleNew = "https://phanmemnhansu.timviec365.vn/setSampleNew"; // thiết lập tin mẫu
        public const string addRecuitmentNew = "https://phanmemnhansu.timviec365.vn/addRecuitmentNew"; // thêm mới tin tuyển dụng
        public const string editRecruitmentNew = "https://phanmemnhansu.timviec365.vn/editRecuitmentNew"; // cập nhật tin tuyển dụng

        // api danh sách ứng viên
        public const string list_candidate = "https://phanmemnhansu.timviec365.vn/listCandidate";
        public const string list_candidate_schedule = "https://phanmemnhansu.timviec365.vn/listCandidateSchedule";
        public const string list_candidate_get_job = "https://phanmemnhansu.timviec365.vn/listCandidateGetJob";
        public const string list_candidate_fail_job = "https://phanmemnhansu.timviec365.vn/listCandidateFailJob";
        public const string list_candidate_cancel_job = "https://phanmemnhansu.timviec365.vn/listCandidateCancelJob";
        public const string list_candidate_contact_job = "https://phanmemnhansu.timviec365.vn/listCandidateContactJob";
        public const string detail_candidate = "https://phanmemnhansu.timviec365.vn/detailCandidate";
        public const string detail_candidate_get_job = "https://phanmemnhansu.timviec365.vn/detailCandidateGetJob";
        public const string detail_candidate_fail_job = "https://phanmemnhansu.timviec365.vn/detailCandidateFailJob";
        public const string detail_candidate_cancel_job = "https://phanmemnhansu.timviec365.vn/detailCandidateCancelJob";
        public const string detail_candidate_contract_job = "https://phanmemnhansu.timviec365.vn/detailCandidateContractJob";
        public const string api_cv = "https://phanmemnhansu.timviec365.vn/upload/cv/"; // api cv ứng viên
        public const string delete_candidate = "https://phanmemnhansu.timviec365.vn/softDeleteCan"; // api xóa ứng viên
        public const string list_all_employee = "https://chamcong.24hpay.vn/service/list_all_employee_of_company.php?filter_by[active]=true&id_com="; // danh sách nhân viên active
        public const string list_all_employee2 = "https://chamcong.24hpay.vn/service/list_all_employee_of_company.php?&id_com="; // danh sách nhân viên active + not active
        public const string list_process_interview = "https://phanmemnhansu.timviec365.vn/listProcessInterview"; // danh sách giai đoạn tuyển dụng
        public const string listProcessInterviewGetJob = "https://phanmemnhansu.timviec365.vn/listProcessInterviewGetJob"; // danh sách nhận việc
        public const string listProcessInterviewFailJob = "https://phanmemnhansu.timviec365.vn/listProcessInterviewFailJob"; // danh sách trượt
        public const string listProcessInterviewCancelJob = "https://phanmemnhansu.timviec365.vn/listProcessInterviewCancelJob"; // danh sách hủy
        public const string listProcessInterviewContactJob = "https://phanmemnhansu.timviec365.vn/listProcessInterviewContactJob"; // danh sách ký hợp đồng
        public const string detail_candidate_process = "https://phanmemnhansu.timviec365.vn/detailCandidateProcess"; // chi tiết ứng viên giai đoạn tuyển dụng
        public const string add_process_interview = "https://phanmemnhansu.timviec365.vn/addProcessInterview"; // thêm mới giai đoạnt tuyển dụng
        public const string delete_process_interview = "https://phanmemnhansu.timviec365.vn/deleteProcessInterview"; // xóa giai đoạn tuyển dụng
        public const string edit_process_interview = "https://phanmemnhansu.timviec365.vn/editProcessInterview"; // sửa giai đoạn tuyển dụng
        public const string add_candidate = "https://phanmemnhansu.timviec365.vn/addCandidate"; // thêm mới ứng viên
        public const string transport_to_interview = "https://phanmemnhansu.timviec365.vn/addCandidateProcessInterview"; // chuyển hồ sơ sang phỏng vấn
        public const string transport_to_get_job = "https://phanmemnhansu.timviec365.vn/addCandidateGetJob"; // chuyển hồ sơ sang nhận việc
        public const string transport_to_fail_job = "https://phanmemnhansu.timviec365.vn/addCandidateFailJob"; // chuyển hồ sơ sang trượt
        public const string transport_to_cancel_job = "https://phanmemnhansu.timviec365.vn/addCandidateCancelJob"; // chuyển hồ sơ sang hủy
        public const string transport_to_contract_job = "https://phanmemnhansu.timviec365.vn/addCandidateContactJob"; // chuyển hồ sơ sang kí hợp đồng
        public const string list_all_new = "https://phanmemnhansu.timviec365.vn/listAllNew"; // lấy tất cả tin tuyển dụng
        public const string edit_candidate_send_cv = "https://phanmemnhansu.timviec365.vn/updateCandidate"; // sửa ứng viên gửi hồ sơ
        public const string edit_candidate_interview = "https://phanmemnhansu.timviec365.vn/apiUpdateScheduleInterview"; // sửa ứng viên giai đoạn phỏng vấn
        public const string edit_candidate_get_job = "https://phanmemnhansu.timviec365.vn/apiUpdateGetJob"; // sửa ứng viên giai đoạn nhận việc
        public const string edit_candidate_fail_job = "https://phanmemnhansu.timviec365.vn/apiUpdateFailJob"; // sửa ứng viên giai đoạn trượt
        public const string edit_candidate_cancel_job = "https://phanmemnhansu.timviec365.vn/apiUpdateCancelJob"; // sửa ứng viên giai đoạn hủy
        public const string edit_candidate_contract_job = "https://phanmemnhansu.timviec365.vn/apiUpdateContactJob"; // sửa ứng viên giai đoạn kí hợp đồng

        //chuyển giai đoạn danh sách ứng viên
        public const string addCandidateGetJob = "https://phanmemnhansu.timviec365.vn/addCandidateGetJob"; //nhận việc
        public const string addCandidateFailJob = "https://phanmemnhansu.timviec365.vn/addCandidateFailJob";  //trượt
        public const string addCandidateCancelJob = "https://phanmemnhansu.timviec365.vn/addCandidateCancelJob"; //hủy
        public const string addCandidateContactJob = "https://phanmemnhansu.timviec365.vn/addCandidateContactJob"; //ký hợp đồng
        public const string addCandidateProcessInterview = "https://phanmemnhansu.timviec365.vn/addCandidateProcessInterview"; //chuyển hồ sơ sang phỏng vấn
        // api kho ứng viên
        public const string list_candidate_depot = "https://phanmemnhansu.timviec365.vn/listCandidateDepot"; // lấy danh sách ứng viên 

        //Đào tạo và phát triển
        public const string list_tranning = "https://phanmemnhansu.timviec365.vn/listTrainingProcess?="; //api quy trình đào tạo
        public const string list_JobPositon = "https://phanmemnhansu.timviec365.vn/listJobDescription"; //api danh sách vị trí công việc
        public const string updateStageTrainingProcess = "https://phanmemnhansu.timviec365.vn/updateStageTrainingProcess"; //api cập nhập giai đoạn

        //Lương thưởng và phúc lợi 
        public const string listAchievement = "https://phanmemnhansu.timviec365.vn/listAchievement"; //api khen thưởng
        public const string listInfringes = "https://phanmemnhansu.timviec365.vn/listInfringes"; //api vi phạm

        //Quản lý hành chính
        // Quản lý nhân viên
        public const string delete_employee = "https://chamcong.24hpay.vn/api_web_hr/delete_employee.php"; // xóa nhân viên
        public const string all_branch = "https://chamcong.24hpay.vn/service/list_child_of_company.php?is_all=true&id_com="; // lấy tất cả chi nhánh của công ty
        public const string edit_employee = "https://chamcong.24hpay.vn/service/company_update_employee_info.php"; //sửa thông tin nhân viên

        // quy định chính sách
        public const string listWorkingRegulations = "https://phanmemnhansu.timviec365.vn/listProvision"; // danh  sách nhóm quy định việc làm
        public const string list_policy_by = "https://phanmemnhansu.timviec365.vn/getListPolicyBy"; // danh sách quy định làm việc
        public const string delete_provision = "https://phanmemnhansu.timviec365.vn/deleteProvision"; // xóa nhóm quy định
        public const string delete_policy = "https://phanmemnhansu.timviec365.vn/deletePolicy"; // xóa quy định
        public const string detail_provison = "https://phanmemnhansu.timviec365.vn/getDetailProvision"; // chi tiết nhóm quy định
        public const string add_provision = "https://phanmemnhansu.timviec365.vn/addProvision"; // thêm mới nhóm quy định
        public const string preview_file_policy = "https://docs.google.com/viewerng/viewer?url=https://phanmemnhansu.timviec365.vn/upload/policy/"; // preview file
        public const string detail_policy = "https://phanmemnhansu.timviec365.vn/getDetailPolicy"; // chi tiết quy định
        public const string update_provision = "https://phanmemnhansu.timviec365.vn/updateProvision"; // update nhóm quy định
        public const string add_policy = "https://phanmemnhansu.timviec365.vn/addPolicy"; // thêm quy định
        public const string edit_policy = "https://phanmemnhansu.timviec365.vn/editPolicy"; // sửa quy định


        public const string listEmployeePolicyPage = "https://phanmemnhansu.timviec365.vn/listEmployePolicy"; //chính sách nhân viên
        public const string list_employee_policy_by = "https://phanmemnhansu.timviec365.vn/getListEmpoyePolicyBy"; // danh sách chính sách nhân viên
        public const string delete_employee_policy = "https://phanmemnhansu.timviec365.vn/deleteEmpoyePolicy"; // xóa nhóm chính sách nhân viên
        public const string delete_employee_policy2 = "https://phanmemnhansu.timviec365.vn/deleteEmpoyePolicySpecific"; // xóa chính sách nhân viên
        public const string detial_employee_policy_group = "https://phanmemnhansu.timviec365.vn/getDetailEpPolicy"; // chi tiết nhóm chính sách nhân viên
        public const string detail_employee_policy = "https://phanmemnhansu.timviec365.vn/getDetailEmployePolicy"; // chi tiết chính sách nhân viên
        public const string add_employee_policy_group = "https://phanmemnhansu.timviec365.vn/addEmpoyePolicy"; // thêm mới nhóm chinh sách nhân viên
        public const string add_employee_policy = "https://phanmemnhansu.timviec365.vn/addEmpoyePolicySpecific"; // thêm mới chính sách nhân viên
        public const string edit_employee_policy_group = "https://phanmemnhansu.timviec365.vn/updateEmpoyePolicy"; // cập nhật nhóm chính sách nhân viên
        public const string edit_employee_policy = "https://phanmemnhansu.timviec365.vn/updateEmpoyePolicySpecific"; // cập nhật chính sách nhân viên

        //Biến động nhân sự
        public const string planning_appointment_list = "https://chamcong.24hpay.vn/api_web_hr/get_list_employee_from_company_appoint.php?filter_by[active]=true&id_com=1761&off_set=0&length=10"; //api danh sách bổ nhiệm quy hoạch
        public const string appointment_list = "https://chamcong.24hpay.vn/api_web_hr/get_list_employee_from_company_appoint.php?filter_by[active]=true&id_com="; // bổ nhiệm, quy hoạch

        public const string updownSlary = "https://tinhluong.timviec365.vn/api_web/list_tang_giam_luong.php?cp=1761&page=0&ep_id=&time1=&time2="; //tăng giảm lương
        public const string up_down_salary = "https://tinhluong.timviec365.vn/api_web/list_tang_giam_luong.php?cp="; // tăng giảm lương
        public const string ListJobRotation = "https://chamcong.24hpay.vn/api_web_hr/get_list_employee_from_company_tranfer_job.php?off_set=0&length=10&id_com=1761"; //Luân chuyển công tác
        public const string list_job_rotation = "https://chamcong.24hpay.vn/api_web_hr/get_list_employee_from_company_tranfer_job.php?id_com="; // luân chuyển công tác



        public const string listDownsizing = "https://chamcong.24hpay.vn/api_web_hr/get_list_employee_from_company_resign.php?id_com=1761"; //Giảm biên chế và nghỉ việc
        public const string list_downsizing = "https://chamcong.24hpay.vn/api_web_hr/get_list_employee_from_company_resign.php?id_com="; // giảm biên chế và nghỉ việc

        public const string getlistEmplouyee = "https://chamcong.24hpay.vn/service/list_all_employee_of_company.php?filter_by[active]=true&id_com="; //api lấy toàn bộ danh sách nhân viên 
        public const string listDepartment = "https://chamcong.24hpay.vn/service/list_department.php?id_com="; //api lấy toàn bộ danh sách phòng ban
        public const string listCompany = "https://chamcong.24hpay.vn/service/list_child_of_company.php?is_all=true&id_com="; //lấy toàn bộ danh sách công ty
        public const string listRecruitmentReport = "https://phanmemnhansu.timviec365.vn/reportNewActive"; //báo cáo chi tiết theo tin tuyển dụng
        public const string listRecruitmentReportEmployee = "https://phanmemnhansu.timviec365.vn/reportListRecruitmentStaff"; //báo cáo chi tiết theo tin tuyển dụng theo nhân viên
        public const string reportlistCandidateRcm = "https://phanmemnhansu.timviec365.vn/reportlistCandidateRcm"; //Báo cáo chi tiết theo nhân viên giới thiệu ứng viên và tiền thưởng trực tiếp
        public const string StatisticalReport = "https://phanmemnhansu.timviec365.vn/totalReport"; //api thống kê kê báo cáo

        public const string EmployeeManager = "https://chamcong.24hpay.vn/service/get_list_employee_from_company.php?filter_by[active]=true&off_set=0&length=10&filter_by[search]=&id_com=1761"; //api quản lý nhân viên

        public const string AddAchievementsPopup = "https://phanmemnhansu.timviec365.vn/addAchievement"; //api thêm thành tích cá nhân
        public const string add_punish_emp = "https://tinhluong.timviec365.vn/api_app/company/add_punish_emp.php";
        public const string UpdateAchievementsPopup = "https://phanmemnhansu.timviec365.vn/updateAchievement"; // api sửa thành tích
        public const string AddAchievementsGroup = "https://phanmemnhansu.timviec365.vn/addAchievementGroup"; // api thêm thành tích tập thể
        public const string updateAchievementGroup = "https://phanmemnhansu.timviec365.vn/updateAchievementGroup"; // api sửa thành tích tập thể
        public const string updateInfingesGroup = "https://phanmemnhansu.timviec365.vn/updateInfingesGroup"; //api thêm mới vi phạm tập thể
        public const string updateInfinges = "https://phanmemnhansu.timviec365.vn/updateInfinges"; //api thêm mới vi phạm cá nhân
        public const string addInfinges = "https://phanmemnhansu.timviec365.vn/addInfinges"; //api thêm mới vi phạm cá nhân
        public const string addInfingesGroup = "https://phanmemnhansu.timviec365.vn/addInfingesGroup"; //api thêm mới vi phạm tập thể

        public const string deleteJobDescription = "https://phanmemnhansu.timviec365.vn/deleteJobDescription"; //Api xóa vị trí công việc
        public const string addJobDescription = " https://phanmemnhansu.timviec365.vn/addJobDescription"; //api thêm mới vị trí công việc
        public const string addTrainingProcess = "https://phanmemnhansu.timviec365.vn/addTrainingProcess"; //api thêm mới vị trí công việc
        public const string listStageTraining = "https://phanmemnhansu.timviec365.vn/listStageTraining"; //api danh sách giai đoạn
        public const string deleteTrainingProcess = "https://phanmemnhansu.timviec365.vn/deleteTrainingProcess"; //api xóa quy trình đào tạo
        public const string addStageTrainingProcess = "https://phanmemnhansu.timviec365.vn/addStageTrainingProcess";//api thêm giai đoạn
        public const string deleteStageTrainingProcess = "https://phanmemnhansu.timviec365.vn/deleteStageTrainingProcess"; //api xóa giai đoạn

        // api báo cáo nhân sự
        public const string list_report_new_active = "https://phanmemnhansu.timviec365.vn/reportNewActive"; // api danh sách chi tiết theo tin tuyển dụng
        public const string reportListRecruitmentStaff = "https://phanmemnhansu.timviec365.vn/reportListRecruitmentStaff"; // api danh sách chi tiết theo nhân viên tuyển dụng

        // dữ liệu đã xóa gần đây
        public const string listDetailDelete = "https://phanmemnhansu.timviec365.vn/listDetailDelete"; // danh sách dữ liệu đã xóa gần đây
        public const string deleteRecentData = "https://phanmemnhansu.timviec365.vn/forceDelete"; // xóa dữ liệu đã xóa gần đây
        public const string restoreRecentData = "https://phanmemnhansu.timviec365.vn/restoreDelete"; // xóa dữ liệu đã xóa gần đây


        public const string addAppoint = "https://chamcong.24hpay.vn/api_web_hr/update_employe_by.php"; // thêm mới quy hoạch bổ nhiệm
        public const string update_employe = "https://chamcong.24hpay.vn/api_web_hr/update_employe_by.php"; //chỉnh sửa quy hoạch bổ nhiệm
        public const string add_working = "https://chamcong.24hpay.vn/api_web_hr/update_employe_tranferjob.php"; //thêm mới luân chuyển công tác

        public const string list_shift = "https://chamcong.24hpay.vn/service/list_shift.php?id_com="; //lấy danh sách ca
        public const string addDownzing = "https://chamcong.24hpay.vn/api_web_hr/update_employe_resign.php"; //thêm mới giảm biên chế nghỉ việc
        public const string updateDownzing = "https://chamcong.24hpay.vn/api_web_hr/update_employe_resign.php"; //cập nhập giảm biên chế nghỉ việc

        // setting
        public const string company_info = "https://chamcong.24hpay.vn/service/detail_company.php?id_com="; // chi tiết công ty
        public const string update_company_info = "https://chamcong.24hpay.vn/service/update_user_info_company.php"; // cập nhật thông tin công ty
        public const string update_employee_info = "https://chamcong.24hpay.vn/service/update_user_info_employee.php"; // cập nhật thông tin nhân viên
        public const string change_pass_company = "https://chamcong.24hpay.vn/service/change_pass_company.php"; // đổi mật khẩu công ty
        public const string change_pass_employee = "https://chamcong.24hpay.vn/service/change_pass_employee.php"; // đổi mật khẩu công ty
        public const string apiCheckRole = "https://phanmemnhansu.timviec365.vn/apiCheckRole"; // check quyền nhân viên
        public const string updateRole = "https://phanmemnhansu.timviec365.vn/updateRole"; // cấp quyền cho nhân viên




        public const string updateAchievement = "https://phanmemnhansu.timviec365.vn/updateAchievement";

        //api thông báo luân chuyển
        public const string NotificationPersonnelChange = "https://mess.timviec365.vn/Notification/NotificationPersonnelChange";
        //api thông báo ken thưởng
        public const string NotificationRewardDiscipline = "https://mess.timviec365.vn/Notification/NotificationRewardDiscipline";
        #endregion
    }
}
