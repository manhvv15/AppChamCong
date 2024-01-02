using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.APIs
{
    public class API
    {
        #region Api New
        public const string listAll_organize = "http://210.245.108.202:3000/api/qlc/organizeDetail/listAll";
        #endregion
        public const string img_source_api = "https://chamcong.24hpay.vn/upload/employee/";
        public const string company_infor_api = "http://210.245.108.202:3000/api/qlc/company/info";

        public const string list_shift_api = "http://210.245.108.202:3000/api/qlc/shift/list";
        public const string create_shift_api = "http://210.245.108.202:3000/api/qlc/shift/create";
        public const string detail_shift_api = "http://210.245.108.202:3000/api/qlc/shift/detail";
        public const string edit_shift_api = "https://api.timviec365.vn/api/qlc/shift/edit";
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
        public const string delete_process = "http://210.245.108.202:3006/api/hr/recruitment/deleteProcess";
        public const string update_process = "http://210.245.108.202:3006/api/hr/recruitment/updateProcess";
        public const string list_process = "http://210.245.108.202:3006/api/hr/recruitment/getListProcess";
        public const string list_candidate_api = "http://210.245.108.202:3006/api/hr/recruitment/listCandi";
        public const string detail_candidate = "https://api.timviec365.vn/api/hr/recruitment/detailCandidateV2";
        public const string detail_candidate_process = "https://api.timviec365.vn/api/hr/recruitment/detailCandidateV2";// chi tiết ứng viên giai đoạn tuyển dụng
        public const string api_cv = "https://phanmemnhansu.timviec365.vn/upload/cv/"; // api cv ứng viên
                                                                                       // api danh sách ứng viên
        public const string list_candidate = "http://210.245.108.202:3006/api/hr/recruitment/wflistCandidate";
        public const string list_candidate_schedule = "http://210.245.108.202:3006/api/hr/recruitment/wflistCandidateSchedule";
        public const string list_candidate_get_job = "http://210.245.108.202:3006/api/hr/recruitment/wflistCandidateGetJob";
        public const string list_candidate_fail_job = "http://210.245.108.202:3006/api/hr/recruitment/wflistCandidateFailJob";
        public const string list_candidate_cancel_job = "http://210.245.108.202:3006/api/hr/recruitment/wflistCandidateCancelJob";
        public const string list_candidate_contact_job = "http://210.245.108.202:3006/api/hr/recruitment/wflistCandidateContactJob";

        public const string detail_candidate_get_job = "http://210.245.108.202:3006/api/hr/recruitment/detailCandidateGetJob";
        public const string detail_candidate_fail_job = "http://210.245.108.202:3006/api/hr/recruitment/detailCandidateFailJob";
        public const string detail_candidate_cancel_job = "http://210.245.108.202:3006/api/hr/recruitment/detailCandidateCancelJob";
        public const string detail_candidate_contract_job = "http://210.245.108.202:3006/api/hr/recruitment/detailCandidateContactJob";

        public const string delete_candidate = "http://210.245.108.202:3006/api/hr/recruitment/softDeleteCandi"; // api xóa ứng viên
        public const string list_all_employee = "https://api.timviec365.vn/api/qlc/managerUser/listUser"; // danh sách nhân viên active
        public const string list_all_employee2 = "http://210.245.108.202:3000/api/qlc/managerUser/listAll"; // danh sách nhân viên active + not active
        public const string list_process_interview = "http://210.245.108.202:3006/api/hr/recruitment/listProcessInterview"; // danh sách giai đoạn tuyển dụng
        public const string listProcessInterviewGetJob = "http://210.245.108.202:3006/api/hr/recruitment/listProcessInterviewGetJob"; // danh sách nhận việc
        public const string listProcessInterviewFailJob = "http://210.245.108.202:3006/api/hr/recruitment/listProcessInterviewFailJob"; // danh sách trượt
        public const string listProcessInterviewCancelJob = "http://210.245.108.202:3006/api/hr/recruitment/listProcessInterviewCancelJob"; // danh sách hủy
        public const string listProcessInterviewContactJob = "http://210.245.108.202:3006/api/hr/recruitment/listProcessInterviewContactJob"; // danh sách ký hợp đồng
        public const string add_process_interview = "http://210.245.108.202:3006/api/hr/recruitment/createProcess"; // thêm mới giai đoạnt tuyển dụng
        public const string delete_process_interview = "http://210.245.108.202:3006/api/hr/recruitment/deleteProcess"; // xóa giai đoạn tuyển dụng
        public const string edit_process_interview = "http://210.245.108.202:3006/api/hr/recruitment/updateProcess"; // sửa giai đoạn tuyển dụng
        public const string add_candidate = "http://210.245.108.202:3006/api/hr/recruitment/createCandidate"; // thêm mới ứng viên
        public const string list_all_new = "http://210.245.108.202:3006/api/hr/recruitment/listNews"; // lấy tất cả tin tuyển dụng
        public const string edit_candidate_send_cv = "http://210.245.108.202:3006/api/hr/recruitment/updateCandidate"; // sửa ứng viên gửi hồ sơ
        public const string edit_candidate_interview = "http://210.245.108.202:3006/api/hr/recruitment/scheduleInter"; // sửa ứng viên giai đoạn phỏng vấn
        public const string edit_candidate_get_job = "http://210.245.108.202:3006/api/hr/recruitment/addCandidateGetJob"; // sửa ứng viên giai đoạn nhận việc
        public const string edit_candidate_fail_job = "http://210.245.108.202:3006/api/hr/recruitment/FailJob"; // sửa ứng viên giai đoạn trượt
        public const string edit_candidate_cancel_job = "http://210.245.108.202:3006/api/hr/recruitment/cancelJob"; // sửa ứng viên giai đoạn hủy
        public const string edit_candidate_contract_job = "http://210.245.108.202:3006/api/hr/recruitment/contactJob"; // sửa ứng viên giai đoạn kí hợp đồng




        public const string edit_description_api = "http://210.245.108.202:3006/api/hr/organizationalStructure/updateDescription";
        public const string organizationalStructure_description = "http://210.245.108.202:3006/api/hr/organizationalStructure/description";
        public const string organizationalStructure_listEp = "http://210.245.108.202:3006/api/hr/organizationalStructure/listEmUntimed";

        public const string vanthu_setting_api = "http://210.245.108.202:3005/api/vanthu/setting/editSetting";
        public const string vanthu_setting_getData_api = "http://210.245.108.202:3005/api/vanthu/setting/createF";

        public const string TamUng_api = "http://210.245.108.202:3005/api/vanthu/catedx/tamung";
        public const string ChiTietDeXuat = "http://210.245.108.202:3005/api/vanthu/catedx/showCTDX";


        public const string showlistcate_api = "http://210.245.108.202:3005/api/vanthu/catedx/showlistcate";










        //Phần nhân viên 
        public const string dexuat_ShowHome = "http://210.245.108.202:3005/api/vanthu/catedx/showHome";






        #region HR
        // api đăng nhập
        public const string login_employee_api = "http://210.245.108.202:3000/api/qlc/employee/login";
        public const string login_company_api = "http://210.245.108.202:3000/api/qlc/employee/login"; //api đăng nhập công ty
        public const string send_otp_password_api = "https://chamcong.24hpay.vn/service/send_otp.php"; //api lấy mã xác nhận
        public const string forgot_password_employee_api = "https://chamcong.24hpay.vn/service/forget_pass_employee.php"; //api đặt lại mật khẩu nhân viên
        public const string forgot_password_company_api = "https://chamcong.24hpay.vn/service/forget_pass_company.php"; //api đặt lại mật khẩu công ty
        public const string verify_otp = "https://chamcong.24hpay.vn/service/verify_otp.php";

        // api trang chủ
        public const string avatar_uri = "https://chamcong.24hpay.vn/upload/employee/";
        public const string logo_uri = "https://chamcong.24hpay.vn/upload/company/logo/";
        public const string achievements_total_per_month = "http://210.245.108.202:3006/api/hr/home/totalAchievement";
        public const string violation_total_per_month = "http://210.245.108.202:3006/api/hr/home/totalInfringe";
        public const string candidate_total = "http://210.245.108.202:3006/api/hr/home/totalCandidate";
        public const string interview_total = "http://210.245.108.202:3006/api/hr/home/totalInterview";
        public const string offerjob_total = "http://210.245.108.202:3006/api/hr/home/totalOfferJob";
        public const string weather_api = "https://phanmemnhansu.timviec365.vn/apiWeather";

        // api quản lý tuyển dụng
        // api quy trình tuyển dụng
        public const string list_recuitment = "http://210.245.108.202:3006/api/hr/recruitment/getRecruitment";
        public const string list_recruitment_stage = "http://210.245.108.202:3006/api/hr/recruitment/getStage";
        public const string delete_recuitment = "http://210.245.108.202:3006/api/hr/recruitment/softDelete"; // xóa quy trình tuyển dụng
        public const string delete_stage = "http://210.245.108.202:3006/api/hr/recruitment/softDeleteStage"; // xóa giai đoạn tuyển dụng
        public const string edit_recruitment = "http://210.245.108.202:3006/api/hr/recruitment/updateRecruitment"; // chỉnh sửa quy trình
        public const string edit_stage = "http://210.245.108.202:3006/api/hr/recruitment/updateStage"; // chỉnh sửa giai đoạn tuyển dụng
        public const string add_stage = "http://210.245.108.202:3006/api/hr/recruitment/createStage"; // thêm mới giai đoạn tuyển dụng

        // api thực hiện tuyển dụng
        public const string total_candidate_by = "http://210.245.108.202:3006/api/hr/recruitment/totalCandi";
        public const string lits_new_active = "http://210.245.108.202:3006/api/hr/recruitment/listNewActive";
        public const string list_schedule = "http://210.245.108.202:3006/api/hr/recruitment/listSchedule";
        public const string list_news = "http://210.245.108.202:3006/api/hr/recruitment/listNews";
        public const string perform_detail = "http://210.245.108.202:3006/api/hr/recruitment/detailNews";
        public const string removeRcruitmentNew = "http://210.245.108.202:3006/api/hr/recruitment/softDeleteNews"; // gỡ tin tuyển dụng
        public const string setSampleNew = "http://210.245.108.202:3006/api/hr/recruitment/createSampleNews"; // thiết lập tin mẫu
        public const string addRecuitmentNew = "http://210.245.108.202:3006/api/hr/recruitment/createNews"; // thêm mới tin tuyển dụng
        public const string editRecruitmentNew = "http://210.245.108.202:3006/api/hr/recruitment/updateNews"; // cập nhật tin tuyển dụng



        //chuyển giai đoạn danh sách ứng viên
        public const string addCandidateGetJob = "http://210.245.108.202:3006/api/hr/recruitment/addCandidateGetJob"; //nhận việc
        public const string addCandidateFailJob = "http://210.245.108.202:3006/api/hr/recruitment/FailJob";  //trượt
        public const string addCandidateCancelJob = "http://210.245.108.202:3006/api/hr/recruitment/cancelJob"; //hủy
        public const string addCandidateContactJob = "http://210.245.108.202:3006/api/hr/recruitment/contactJob"; //ký hợp đồng
        public const string addCandidateProcessInterview = "http://210.245.108.202:3006/api/hr/recruitment/scheduleInter"; //chuyển hồ sơ sang phỏng vấn
        // api kho ứng viên
        public const string list_candidate_depot = "http://210.245.108.202:3006/api/hr/recruitment/listCandi"; // lấy danh sách ứng viên 

        //Đào tạo và phát triển
        public const string list_tranning = "http://210.245.108.202:3006/api/hr/training/listProcessTrain"; //api quy trình đào tạo
        public const string list_JobPositon = "http://210.245.108.202:3006/api/hr/training/listJob"; //api danh sách vị trí công việc
        public const string updateStageTrainingProcess = "http://210.245.108.202:3006/api/hr/training/updateStage"; //api cập nhập giai đoạn

        //Lương thưởng và phúc lợi 
        public const string listAchievement = "http://210.245.108.202:3006/api/hr/welfare/listAchievement"; //api khen thưởng
        public const string listInfringes = "http://210.245.108.202:3006/api/hr/welfare/listInfinges"; //api vi phạm

        //Quản lý hành chính
        // Quản lý nhân viên
        public const string delete_employee = "http://210.245.108.202:3000/api/qlc/managerUser/del"; // xóa nhân viên
        public const string all_branch = "http://210.245.108.202:3000/api/qlc/company/child/list"; // lấy tất cả chi nhánh của công ty
        public const string edit_employee = "http://210.245.108.202:3000/api/qlc/employee/updateInfoEmployeeComp"; //sửa thông tin nhân viên

        // quy định chính sách
        public const string listWorkingRegulations = "http://210.245.108.202:3006/api/hr/administration/listProvision"; // danh  sách nhóm quy định việc làm
        public const string list_policy_by = "http://210.245.108.202:3006/api/hr/administration/listPolicy"; // danh sách quy định làm việc
        public const string delete_provision = "http://210.245.108.202:3006/api/hr/administration/deleteProvision"; // xóa nhóm quy định
        public const string delete_policy = "http://210.245.108.202:3006/api/hr/administration/deletePolicy"; // xóa quy định
        public const string detail_provison = "http://210.245.108.202:3006/api/hr/administration/detailProvision"; // chi tiết nhóm quy định
        public const string add_provision = "http://210.245.108.202:3006/api/hr/administration/addProvision"; // thêm mới nhóm quy định
        public const string preview_file_policy = "https://docs.google.com/viewerng/viewer?url=https://phanmemnhansu.timviec365.vn/upload/policy/"; // preview file
        public const string detail_policy = "http://210.245.108.202:3006/api/hr/administration/detailPolicy"; // chi tiết quy định
        public const string update_provision = "http://210.245.108.202:3006/api/hr/administration/updateProvision"; // update nhóm quy định
        public const string add_policy = "http://210.245.108.202:3006/api/hr/administration/addPolicy"; // thêm quy định
        public const string edit_policy = "http://210.245.108.202:3006/api/hr/administration/updatePolicy"; // sửa quy định


        public const string listEmployeePolicyPage = "http://210.245.108.202:3006/api/hr/administration/listEmpoyePolicy"; //chính sách nhân viên
        public const string list_employee_policy_by = "http://210.245.108.202:3006/api/hr/administration/listEmployeePolicySpecific"; // danh sách chính sách nhân viên
        public const string delete_employee_policy = "http://210.245.108.202:3006/api/hr/administration/deleteEmployeePolicy"; // xóa nhóm chính sách nhân viên
        public const string delete_employee_policy2 = "http://210.245.108.202:3006/api/hr/administration/deleteEmployeePolicySpecific"; // xóa chính sách nhân viên
        public const string detial_employee_policy_group = "http://210.245.108.202:3006/api/hr/administration/getDetailPolicy"; // chi tiết nhóm chính sách nhân viên
        public const string detail_employee_policy = "http://210.245.108.202:3006/api/hr/administration/detailEmployeePolicySpecific"; // chi tiết chính sách nhân viên
        public const string add_employee_policy_group = "http://210.245.108.202:3006/api/hr/administration/addEmployeePolicy"; // thêm mới nhóm chinh sách nhân viên
        public const string add_employee_policy = "http://210.245.108.202:3006/api/hr/administration/addEmpoyePolicySpecific"; // thêm mới chính sách nhân viên
        public const string edit_employee_policy_group = "http://210.245.108.202:3006/api/hr/administration/updateEmployeePolicy"; // cập nhật nhóm chính sách nhân viên
        public const string edit_employee_policy = "http://210.245.108.202:3006/api/hr/administration/updateEmployeePolicySpecific"; // cập nhật chính sách nhân viên

        //Biến động nhân sự
        public const string appointment_list = "http://210.245.108.202:3006/api/hr/personalChange/getListAppoint"; // bổ nhiệm, quy hoạch

        public const string up_down_salary = "http://210.245.108.202:3006/api/hr/report/reportDetail"; // tăng giảm lương
        public const string list_job_rotation = "http://210.245.108.202:3006/api/hr/personalChange/getListTranferJob"; // luân chuyển công tác



        public const string list_downsizing = "http://210.245.108.202:3006/api/hr/personalChange/getListQuitJob"; // giảm biên chế và nghỉ việc

        public const string getlistEmplouyee = "http://210.245.108.202:3000/api/qlc/managerUser/list"; //api lấy toàn bộ danh sách nhân viên 
        public const string listDepartment = "http://210.245.108.202:3000/api/qlc/department/list"; //api lấy toàn bộ danh sách phòng ban
        public const string listCompany = "http://210.245.108.202:3000/api/qlc/company/child/list"; //lấy toàn bộ danh sách công ty
        public const string listRecruitmentReport = "http://210.245.108.202:3006/api/hr/report/reportDetailRecruitment"; //báo cáo chi tiết theo tin tuyển dụng
        public const string listRecruitmentReportEmployee = "http://210.245.108.202:3006/api/hr/report/reportHr"; //báo cáo chi tiết theo tin tuyển dụng theo nhân viên
        public const string reportlistCandidateRcm = "http://210.245.108.202:3006/api/hr/report/reportDetailHRAndAchievements"; //Báo cáo chi tiết theo nhân viên giới thiệu ứng viên và tiền thưởng trực tiếp
        public const string StatisticalReport = "http://210.245.108.202:3006/api/hr/report/reportRecruitment"; //api thống kê kê báo cáo


        public const string AddAchievementsPopup = "http://210.245.108.202:3006/api/hr/welfare/addAchievement"; //api thêm thành tích cá nhân
        public const string UpdateAchievementsPopup = "http://210.245.108.202:3006/api/hr/welfare/updateAchievement"; // api sửa thành tích
        public const string AddAchievementsGroup = "http://210.245.108.202:3006/api/hr/welfare/addAchievementGroup"; // api thêm thành tích tập thể
        public const string updateAchievementGroup = "http://210.245.108.202:3006/api/hr/welfare/updateAchievement"; // api sửa thành tích tập thể
        public const string updateInfingesGroup = "http://210.245.108.202:3006/api/hr/welfare/updateInfinges"; //api thêm mới vi phạm tập thể
        public const string updateInfinges = "http://210.245.108.202:3006/api/hr/welfare/updateInfinges"; //api Chỉnh sửa vi phạm cá nhân
        public const string addInfinges = "http://210.245.108.202:3006/api/hr/welfare/addInfinges"; //api thêm mới vi phạm cá nhân
        public const string addInfingesGroup = "http://210.245.108.202:3006/api/hr/welfare/addInfingesGroup"; //api thêm mới vi phạm tập thể

        public const string deleteJobDescription = "http://210.245.108.202:3006/api/hr/training/jobSoftDelete"; //Api xóa vị trí công việc
        public const string addJobDescription = " http://210.245.108.202:3006/api/hr/training/createJob"; //api thêm mới vị trí công việc
        public const string addTrainingProcess = "http://210.245.108.202:3006/api/hr/training/process"; //api thêm mới quy trình đào tạo
        public const string listStageTraining = "http://210.245.108.202:3006/api/hr/training/detailProcess"; //api danh sách giai đoạn
        public const string deleteTrainingProcess = "http://210.245.108.202:3006/api/hr/training/softDeleteProcess"; //api xóa quy trình đào tạo
        public const string addStageTrainingProcess = "http://210.245.108.202:3006/api/hr/training/stage";//api thêm giai đoạn
        public const string deleteStageTrainingProcess = "http://210.245.108.202:3006/api/hr/training/softDeleteStage"; //api xóa giai đoạn

        // api báo cáo nhân sự
        public const string list_report_new_active = "http://210.245.108.202:3006/api/hr/report/reportDetailRecruitment"; // api danh sách chi tiết theo tin tuyển dụng
        public const string reportListRecruitmentStaff = "http://210.245.108.202:3006/api/hr/report/reportHr"; // api danh sách chi tiết theo nhân viên tuyển dụng

        // dữ liệu đã xóa gần đây
        public const string listDetailDelete = "http://210.245.108.202:3006/api/hr/forceDelete/listDetailDelete"; // danh sách dữ liệu đã xóa gần đây
        public const string deleteRecentData = "http://210.245.108.202:3006/api/hr/forceDelete/delete"; // xóa dữ liệu đã xóa gần đây
        public const string restoreRecentData = "http://210.245.108.202:3006/api/hr/forceDelete/restoreDelete"; // xóa dữ liệu đã xóa gần đây


        public const string addAppoint = "http://210.245.108.202:3006/api/hr/personalChange/updateAppoint"; // thêm mới quy hoạch bổ nhiệm
        public const string update_employe = "http://210.245.108.202:3006/api/hr/personalChange/updateAppoint"; //chỉnh sửa quy hoạch bổ nhiệm
        public const string add_working = "http://210.245.108.202:3006/api/hr/personalChange/updateTranferJob"; //thêm mới luân chuyển công tác

        public const string list_shift = "http://210.245.108.202:3000/api/qlc/shift/list?com_id="; //lấy danh sách ca
        public const string addDownzing = "http://210.245.108.202:3006/api/hr/personalChange/updateQuitJob"; //thêm mới giảm biên chế nghỉ việc
        public const string updateDownzing = "http://210.245.108.202:3006/api/hr/personalChange/updateQuitJob"; //cập nhập giảm biên chế nghỉ việc

        // setting
        public const string company_info = "http://210.245.108.202:3000/api/qlc/company/info"; // chi tiết công ty
        public const string update_company_info = "http://210.245.108.202:3000/api/qlc/Company/updateInfoCompany"; // cập nhật thông tin công ty
        public const string update_employee_info = "https://chamcong.24hpay.vn/service/update_user_info_employee.php"; // cập nhật thông tin nhân viên
        public const string change_pass_company = "http://210.245.108.202:3000/api/qlc/Company/updateNewPassword"; // đổi mật khẩu công ty
        public const string change_pass_employee = "http://210.245.108.202:3000/api/qlc/employee/updatePassword"; // đổi mật khẩu công ty
        public const string apiCheckRole = "http://210.245.108.202:3006/api/hr/setting/listPermision"; // check quyền nhân viên
        public const string updateRole = "http://210.245.108.202:3006/api/hr/setting/permision"; // cấp quyền cho nhân viên




        public const string updateAchievement = "http://210.245.108.202:3006/api/hr/welfare/updateAchievement";

        //api thông báo luân chuyển
        public const string NotificationPersonnelChange = "https://mess.timviec365.vn/Notification/NotificationPersonnelChange";
        //api thông báo ken thưởng
        public const string NotificationRewardDiscipline = "https://mess.timviec365.vn/Notification/NotificationRewardDiscipline";

        #endregion

        public const string transport_to_get_job = "https://phanmemnhansu.timviec365.vn/addCandidateGetJob"; // chuyển hồ sơ sang nhận việc
        public const string transport_to_fail_job = "https://phanmemnhansu.timviec365.vn/addCandidateFailJob"; // chuyển hồ sơ sang trượt
        public const string transport_to_cancel_job = "https://phanmemnhansu.timviec365.vn/addCandidateCancelJob"; // chuyển hồ sơ sang hủy
        public const string transport_to_contract_job = "https://phanmemnhansu.timviec365.vn/addCandidateContactJob"; // chuyển hồ sơ sang kí hợp đồng

        //Biến động nhân sự
        public const string planning_appointment_list = "https://chamcong.24hpay.vn/api_web_hr/get_list_employee_from_company_appoint.php?filter_by[active]=true&id_com=1761&off_set=0&length=10"; //api danh sách bổ nhiệm quy hoạch

        public const string updownSlary = "https://tinhluong.timviec365.vn/api_web/list_tang_giam_luong.php?cp=1761&page=0&ep_id=&time1=&time2="; //tăng giảm lương
        public const string ListJobRotation = "https://chamcong.24hpay.vn/api_web_hr/get_list_employee_from_company_tranfer_job.php?off_set=0&length=10&id_com=1761"; //Luân chuyển công tác



        public const string listDownsizing = "https://chamcong.24hpay.vn/api_web_hr/get_list_employee_from_company_resign.php?id_com=1761"; //Giảm biên chế và nghỉ việc

        public const string EmployeeManager = "https://chamcong.24hpay.vn/service/get_list_employee_from_company.php?filter_by[active]=true&off_set=0&length=10&filter_by[search]=&id_com=1761"; //api quản lý nhân viên

        public const string add_punish_emp = "https://tinhluong.timviec365.vn/api_app/company/add_punish_emp.php";


        #region API Function Chấm Công
        //Cài đặt wifi và Ip
        public const string list_wifi_api = "https://api.timviec365.vn/api/qlc/SettingWifi/list";
        public const string add_wifi_api = "http://210.245.108.202:3000/api/qlc/SettingWifi/create";
        public const string edit_wifi_api = "http://210.245.108.202:3000/api/qlc/SettingWifi/update";
        public const string delete_wifi_api = "http://210.245.108.202:3000/api/qlc/SettingWifi/delete";

        public const string list_ip_api = "http://210.245.108.202:3000/api/qlc/SetIp/get";
        public const string edit_ip_api = "http://210.245.108.202:3000/api/qlc/SetIp/edit";
        public const string create_ip_api = "http://210.245.108.202:3000/api/qlc/SetIp/create";
        public const string delete_ip_api = "http://210.245.108.202:3000/api/qlc/SetIp/delete";

        //Lịch Làm Việc
        public const string list_saff_in_Calendar_Work_api = "http://210.245.108.202:3000/api/qlc/cycle/list_employee";
        public const string List_All_Calendar_Work = "http://210.245.108.202:3000/api/qlc/cycle/list";
        public const string list_shifts_api = "http://210.245.108.202:3000/api/qlc/shift/list";
        public const string Edit_Calendar_Api = "http://210.245.108.202:3000/api/qlc/cycle/edit";
        public const string Add_Calendar_api = "http://210.245.108.202:3000/api/qlc/cycle/create";
        public const string delete_Calendar_Work = "http://210.245.108.202:3000/api/qlc/cycle/del";
        public const string List_Saff_Api = "http://210.245.108.202:3000/api/qlc/managerUser/list";
        public const string Add_Saff_In_CalendarWork_Api = "http://210.245.108.202:3000/api/qlc/cycle/add_employee";
        public const string Create_Calendar_Work = "http://210.245.108.202:3000/api/qlc/cycle/create";
        public const string Coppy_CalendarWork_Api = "http://210.245.108.202:3000/api/qlc/cycle/copy";
        public const string Delete_SaffInCalendar_Api = "http://210.245.108.202:3000/api/qlc/cycle/delete_employee";
        public const string List_Ep_not_in_Calendar_api = "https://api.timviec365.vn/api/qlc/cycle/list_not_in_cycle";
        //Công Chuẩn
        public const string list_CongChuan_api = "http://210.245.108.202:3000/api/qlc/companyworkday/detail";
        public const string Create_CongChuan_Api = "http://210.245.108.202:3000/api/qlc/companyworkday/create";
        public const string List_ChillCompany_Api = "http://210.245.108.202:3000/api/qlc/company/child/list";
        public const string List_Room_Api = "http://210.245.108.202:3000/api/qlc/department/list";

        //Cấu hình chấm công
        public const string ChiTiet_CongTy_Api = "http://210.245.108.202:3000/api/qlc/company/info";
        public const string CapNhat_CauHinh_Api = "http://210.245.108.202:3000/api/qlc/company/update_type_timekeeping";
        public const string CauHinh_ChamCong_Api = "http://210.245.108.202:3000/api/qlc/company/update_way_timekeeping";

        // Cập nhật khuôn mặt
        public const string List_UpdateFace_Api = "http://210.245.108.202:3000/api/qlc/face/list";
        public const string List_ManagerSaff_Api = "http://210.245.108.202:3000/api/qlc/managerUser/list";
        public const string List_Position_Api = "http://210.245.108.202:3006/api/hr/organizationalStructure/listPosition";
        public const string Duyet_KhuonMat_Api = "http://210.245.108.202:3000/api/qlc/face/add";

        //Duyet Thiết bị mới
        public const string Duyet_ThietBi_Api = "http://210.245.108.202:3000/api/qlc/checkdevice/list";
        public const string Xoa_DuyetTB_Api = "http://210.245.108.202:3000/api/qlc/checkdevice/delete";
        public const string XacNhan_Duyet_Api = "http://210.245.108.202:3000/api/qlc/checkdevice/add";

        //Xuất công
        public const string XuatCong_Api = "http://210.245.108.202:3000/api/qlc/timekeeping/com/success";

        //Cảm xúc
        public const string DanhSach_CamXuc_Api = "http://210.245.108.202:3000/api/qlc/emotions/list";
        public const string ThemMoi_CamSuc_Api = "http://210.245.108.202:3000/api/qlc/emotions/create";
        public const string CapNhat_CamXuc_Api = "http://210.245.108.202:3000/api/qlc/emotions/update";
        public const string Xoa_CamXuc_Api = "http://210.245.108.202:3000/api/qlc/emotions/delete";
        public const string CapNhat_DiemChuan_Api = "http://210.245.108.202:3000/api/qlc/emotions/updateMinScore";
        public const string OnOff_CamXuc_Api = "http://210.245.108.202:3000/api/qlc/emotions/toggleOnOff";

        //Lịch sử chấm công
        public const string LichSuChamCong_Api = "http://210.245.108.202:3000/api/qlc/timekeeping/get_history_time_keeping_by_company";

        //Chấm công khuôn mặt
        public const string DiemDanh_KhuonMat_Api = "http://210.245.108.202:3000/api/qlc/timekeeping/create/web";
        public const string ThongTin_NhanVien_Api = "http://210.245.108.202:3000/api/qlc/shift/list_shift_user";
        public const string KiemTra_KhuonMat_Api = "http://43.239.223.147:5001/verify_web";
        #endregion
    }
}
