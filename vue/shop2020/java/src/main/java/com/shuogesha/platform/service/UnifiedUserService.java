package com.shuogesha.platform.service;

import java.util.Map;

import com.shuogesha.platform.web.exception.BadCredentialsException;
import com.shuogesha.platform.web.exception.UsernameNotFoundException;
import com.shuogesha.platform.web.mongo.Pagination;
import com.shuogesha.platform.entity.UnifiedUser;

public interface UnifiedUserService {
	Pagination getPage(String name, int pageNo, int pageSize);

	UnifiedUser findById(Long id);

	void save(UnifiedUser bean);

	void update(UnifiedUser bean);

	void removeById(Long id);
	
	void removeByIds(Long[] ids);

	UnifiedUser save(String realname,String username, String password,
			String email, String phone,String sex, String ip, String type,String avatar);

	void updatePassword(String password, Long id);

	void updateLoginInfo(Long uid, String ip);

	void activeLogin(UnifiedUser unifiedUser, String ip);

	UnifiedUser login(String username, String password, String ip) throws UsernameNotFoundException, BadCredentialsException,
	BadCredentialsException;

	UnifiedUser findByUsername(String username);

	void update(String nickName, String avatar, Long unifiedUserId);

	boolean emailNotExist(String username); 
	
	boolean phoneNotExist(String username);

	UnifiedUser register(String nickName, String email, String mobile,
			String password, String ip);
	/**
	 * 注册用户类型
	 * @param nickName
	 * @param string
	 * @param mobile
	 * @param password
	 * @param ip
	 * @param type
	 * @return
	 */
	UnifiedUser registerType(String nickName, String email, String mobile,
			String password, String ip, String type);
	/**
	 * 自定义修改信息
	 * @param id
	 * @param realname
	 * @param username
	 * @param password
	 * @param email
	 * @param phone
	 * @param sex
	 */
	void update(Long id, String realname, String username, String password,
			String email, String phone, String sex,String avatar);

	boolean usernameNotExist(String username);

	void updatePwdByIds(Long[] ids); 
	
	long findMax();
	/**
	 * app用户登录
	 * @param appId
	 * @param unifiedUserId
	 */
	void logout(String appId, Long unifiedUserId);
	/**
	 * 禁用
	 * @param ids
	 * @param b
	 */
	void updateStatus(Long[] ids, boolean b);
	
	UnifiedUser updateProfile(UnifiedUser unifiedUser);
}