package com.shuogesha.platform.service;

import com.shuogesha.platform.web.mongo.Pagination;
import com.shuogesha.platform.entity.User;

public interface UserService {
	Pagination getPage(String name, int pageNo, int pageSize);

	User findById(Long id);

	void save(User bean);

	void update(User bean);

	void removeById(Long id);
	
	void removeByIds(Long[] ids);

	void saveAdmin(User bean, String username, String password, String email,
			String ip);

	boolean usernameNotExist(String username);

	void update(User bean, String password, String email,
			Long id);
	//自定义
	User save(String username,String realname, String password, String email,String phone,String sex,String remark,String ip,String type);
	//自定义
	void update(Long id,String username,String realname, String password, String email,String phone,String sex,String remark);

}