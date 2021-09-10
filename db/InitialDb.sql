-- 创建数据库
create database epmdb;

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for Departments
-- ----------------------------
DROP TABLE IF EXISTS `Departments`;
CREATE TABLE `Departments`  (
  `ClusterID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `ParentID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `IsDeleted` int(11) NOT NULL,
  `Dep` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `ID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `CreateTime` datetime(6) NOT NULL,
  `UpdateUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `UpdateTime` datetime(6) NOT NULL,
  PRIMARY KEY (`ClusterID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Departments
-- ----------------------------
INSERT INTO `Departments` VALUES (3, '行政部', NULL, '行政部门', 1, '[]', '1f6fb27e-378f-451e-b762-f6dfdffd0b22', 'admin', '2021-09-01 10:12:11.707334', 'admin', '2021-09-03 10:36:14.247697');
INSERT INTO `Departments` VALUES (4, '技术中心', NULL, NULL, 0, '[]', 'd5b6ef54-e785-4711-bd38-8092bc8fad7f', 'admin', '2021-09-03 09:37:08.993613', 'admin', '2021-09-03 09:37:08.993613');
INSERT INTO `Departments` VALUES (5, '系统开发部', 'd5b6ef54-e785-4711-bd38-8092bc8fad7f', '4123123', 0, '[\"d5b6ef54-e785-4711-bd38-8092bc8fad7f\"]', '3003eb64-b04d-445e-9ca1-3c3250bc2e4c', 'admin', '2021-09-03 09:37:22.617142', 'admin', '2021-09-03 14:29:35.323945');
INSERT INTO `Departments` VALUES (6, '数据开发部', 'd5b6ef54-e785-4711-bd38-8092bc8fad7f', NULL, 0, '[\"d5b6ef54-e785-4711-bd38-8092bc8fad7f\"]', 'e7b751de-0e2d-4e34-bed5-e3e3d21b32de', 'admin', '2021-09-03 09:37:52.922547', 'admin', '2021-09-03 09:37:52.922547');

-- ----------------------------
-- Table structure for Menus
-- ----------------------------
DROP TABLE IF EXISTS `Menus`;
CREATE TABLE `Menus`  (
  `ClusterID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `ParentMenuID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Type` int(11) NULL DEFAULT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `IsDeleted` int(11) NOT NULL,
  `ParentList` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `ID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `CreateTime` datetime(6) NOT NULL,
  `UpdateUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `UpdateTime` datetime(6) NOT NULL,
  PRIMARY KEY (`ClusterID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 10 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Menus
-- ----------------------------
INSERT INTO `Menus` VALUES (1, '系统管理', '/system', NULL, 1, '', 0, '', '655f0c99-8629-4439-8998-52b772b51e81', 'admin', '2021-09-01 17:27:11.469442', 'admin', '2021-09-01 17:27:11.469439');
INSERT INTO `Menus` VALUES (3, '部门管理', '/system/deptlist', '655f0c99-8629-4439-8998-52b772b51e81', 1, '', 0, '655f0c99-8629-4439-8998-52b772b51e81', '58a78eff-e04f-461d-a464-463f0f363aaf', 'admin', '2021-09-02 10:35:40.926641', 'admin', '2021-09-02 10:35:40.926639');
INSERT INTO `Menus` VALUES (4, '用户管理', '/system/userlist', '655f0c99-8629-4439-8998-52b772b51e81', 1, '', 0, '655f0c99-8629-4439-8998-52b772b51e81', 'cb94bb18-8711-42ae-9af5-82d6c7977ef9', 'admin', '2021-09-02 10:54:20.140792', 'admin', '2021-09-02 10:54:20.140790');
INSERT INTO `Menus` VALUES (5, '菜单管理', '/system/menulist', '655f0c99-8629-4439-8998-52b772b51e81', 1, '', 0, '655f0c99-8629-4439-8998-52b772b51e81', 'fad7d157-d31f-445e-b56e-6c00cf785bb5', 'admin', '2021-09-02 10:54:43.485873', 'admin', '2021-09-02 10:54:43.485870');
INSERT INTO `Menus` VALUES (6, '角色管理', '/system/rolelist', '655f0c99-8629-4439-8998-52b772b51e81', 1, '', 0, '655f0c99-8629-4439-8998-52b772b51e81', '1db5112c-cce7-4da3-ab26-2abd8a1d2674', 'admin', '2021-09-02 10:55:01.553626', 'admin', '2021-09-02 10:55:01.553625');
INSERT INTO `Menus` VALUES (7, '工作记录管理', '/work', NULL, 1, '', 0, '', '60bcd89e-e356-43d0-97d7-2fbff7b0bcc0', 'admin', '2021-09-02 10:56:08.981510', 'admin', '2021-09-02 10:56:08.981508');
INSERT INTO `Menus` VALUES (8, '工作记录', '/work/working', '60bcd89e-e356-43d0-97d7-2fbff7b0bcc0', 1, '', 0, '60bcd89e-e356-43d0-97d7-2fbff7b0bcc0', 'dec6d8b2-20d5-4d70-ad24-3bc7d5bae64e', 'admin', '2021-09-02 10:56:31.690862', 'admin', '2021-09-02 10:56:31.690859');
INSERT INTO `Menus` VALUES (9, '部门工作记录', '/work/authworking', '60bcd89e-e356-43d0-97d7-2fbff7b0bcc0', 1, '', 0, '60bcd89e-e356-43d0-97d7-2fbff7b0bcc0', 'aac7b3d5-619e-41bd-b036-bbbba2cdf62c', '系统管理员', '2021-09-08 11:11:25.487107', '系统管理员', '2021-09-08 11:11:25.487107');

-- ----------------------------
-- Table structure for RoleMenus
-- ----------------------------
DROP TABLE IF EXISTS `RoleMenus`;
CREATE TABLE `RoleMenus`  (
  `ClusterID` int(11) NOT NULL AUTO_INCREMENT,
  `RoleID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `MenuID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `IsDeleted` int(11) NOT NULL,
  `ID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `CreateTime` datetime(6) NOT NULL,
  `UpdateUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `UpdateTime` datetime(6) NOT NULL,
  PRIMARY KEY (`ClusterID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 30 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of RoleMenus
-- ----------------------------
INSERT INTO `RoleMenus` VALUES (1, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', '655f0c99-8629-4439-8998-52b772b51e81', NULL, 1, 'b1ac1b66-c079-4142-80fe-e96b617fe593', 'admin', '2021-09-02 16:15:00.714255', '系统管理员', '2021-09-08 11:11:37.805324');
INSERT INTO `RoleMenus` VALUES (2, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', '58a78eff-e04f-461d-a464-463f0f363aaf', NULL, 1, '0ac21129-6661-4bf0-a892-000eafbd7e7b', 'admin', '2021-09-02 16:15:00.714255', '系统管理员', '2021-09-08 11:11:37.809225');
INSERT INTO `RoleMenus` VALUES (3, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', 'cb94bb18-8711-42ae-9af5-82d6c7977ef9', NULL, 1, 'd3837d63-64d9-41a7-b7e6-c667aece310c', 'admin', '2021-09-02 16:15:00.714255', '系统管理员', '2021-09-08 11:11:37.809802');
INSERT INTO `RoleMenus` VALUES (4, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', 'fad7d157-d31f-445e-b56e-6c00cf785bb5', NULL, 1, '6d5c774c-a4ec-4d22-892f-6d09980a02d7', 'admin', '2021-09-02 16:15:00.714255', '系统管理员', '2021-09-08 11:11:37.810475');
INSERT INTO `RoleMenus` VALUES (5, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', '1db5112c-cce7-4da3-ab26-2abd8a1d2674', NULL, 1, 'e5d80951-782b-48ac-b8e3-0ea919bca966', 'admin', '2021-09-02 16:15:00.714255', '系统管理员', '2021-09-08 11:11:37.811231');
INSERT INTO `RoleMenus` VALUES (6, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', '60bcd89e-e356-43d0-97d7-2fbff7b0bcc0', NULL, 1, 'e0619a98-062f-4fdf-a5aa-29cb519128ab', 'admin', '2021-09-02 16:15:00.714255', '系统管理员', '2021-09-08 11:11:37.814033');
INSERT INTO `RoleMenus` VALUES (7, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', 'dec6d8b2-20d5-4d70-ad24-3bc7d5bae64e', NULL, 1, '3445481e-a2a7-463e-af32-251261b3b39c', 'admin', '2021-09-02 16:15:00.714255', '系统管理员', '2021-09-08 11:11:37.814119');
INSERT INTO `RoleMenus` VALUES (8, 'd70f1763-04e0-418a-b381-22c7bb21a09b', '655f0c99-8629-4439-8998-52b772b51e81', NULL, 1, '69ddddff-6bd7-48cd-aec0-8cfb6e521963', 'admin', '2021-09-03 12:55:40.944501', 'admin', '2021-09-03 13:05:37.393460');
INSERT INTO `RoleMenus` VALUES (9, 'd70f1763-04e0-418a-b381-22c7bb21a09b', '58a78eff-e04f-461d-a464-463f0f363aaf', NULL, 1, '9671dd50-0400-4d51-8e3c-b060dc94e46a', 'admin', '2021-09-03 12:55:40.944501', 'admin', '2021-09-03 13:05:37.393871');
INSERT INTO `RoleMenus` VALUES (10, 'd70f1763-04e0-418a-b381-22c7bb21a09b', 'cb94bb18-8711-42ae-9af5-82d6c7977ef9', NULL, 1, '51359adb-0496-43cd-843e-0ffe93211130', 'admin', '2021-09-03 12:55:40.944501', 'admin', '2021-09-03 13:05:37.393940');
INSERT INTO `RoleMenus` VALUES (11, 'd70f1763-04e0-418a-b381-22c7bb21a09b', 'fad7d157-d31f-445e-b56e-6c00cf785bb5', NULL, 1, '6935d315-6800-417e-8040-fa0bb5b9076e', 'admin', '2021-09-03 12:55:40.944501', 'admin', '2021-09-03 13:05:37.394004');
INSERT INTO `RoleMenus` VALUES (12, 'd70f1763-04e0-418a-b381-22c7bb21a09b', '1db5112c-cce7-4da3-ab26-2abd8a1d2674', NULL, 1, '7a808c36-1db9-426d-b43f-2556ac6fa58b', 'admin', '2021-09-03 12:55:40.944501', 'admin', '2021-09-03 13:05:37.394061');
INSERT INTO `RoleMenus` VALUES (13, 'd70f1763-04e0-418a-b381-22c7bb21a09b', '60bcd89e-e356-43d0-97d7-2fbff7b0bcc0', NULL, 1, 'a4beabc2-0c74-49a6-89e3-79b11f1e4885', 'admin', '2021-09-03 12:55:40.944501', 'admin', '2021-09-03 13:05:37.394105');
INSERT INTO `RoleMenus` VALUES (14, 'd70f1763-04e0-418a-b381-22c7bb21a09b', 'dec6d8b2-20d5-4d70-ad24-3bc7d5bae64e', NULL, 1, 'ec72384a-a42c-4a40-b85d-e21714f605a1', 'admin', '2021-09-03 12:55:40.944501', 'admin', '2021-09-03 13:05:37.394147');
INSERT INTO `RoleMenus` VALUES (15, 'd70f1763-04e0-418a-b381-22c7bb21a09b', '655f0c99-8629-4439-8998-52b772b51e81', NULL, 1, '071f5f4f-8cbf-435d-8eaa-2f48b991279f', 'admin', '2021-09-03 13:05:19.946446', 'admin', '2021-09-03 13:05:37.394188');
INSERT INTO `RoleMenus` VALUES (16, 'd70f1763-04e0-418a-b381-22c7bb21a09b', '58a78eff-e04f-461d-a464-463f0f363aaf', NULL, 1, '129d59a7-1b8d-4b09-93c9-42121ec17ccb', 'admin', '2021-09-03 13:05:19.946446', 'admin', '2021-09-03 13:05:37.394230');
INSERT INTO `RoleMenus` VALUES (17, 'd70f1763-04e0-418a-b381-22c7bb21a09b', 'cb94bb18-8711-42ae-9af5-82d6c7977ef9', NULL, 1, '6f8d3b87-4501-4e4c-a8a0-3c554fe5c15e', 'admin', '2021-09-03 13:05:19.946446', 'admin', '2021-09-03 13:05:37.394275');
INSERT INTO `RoleMenus` VALUES (18, 'd70f1763-04e0-418a-b381-22c7bb21a09b', 'fad7d157-d31f-445e-b56e-6c00cf785bb5', NULL, 1, '742a84c7-afdc-4336-8d38-40b81c3e2098', 'admin', '2021-09-03 13:05:19.946446', 'admin', '2021-09-03 13:05:37.394319');
INSERT INTO `RoleMenus` VALUES (19, 'd70f1763-04e0-418a-b381-22c7bb21a09b', '1db5112c-cce7-4da3-ab26-2abd8a1d2674', NULL, 1, '08858a0a-3cab-4e94-8b67-c1b783f51327', 'admin', '2021-09-03 13:05:19.946446', 'admin', '2021-09-03 13:05:37.394365');
INSERT INTO `RoleMenus` VALUES (20, '81e2c6b0-6ab3-4fbb-923c-c9a1a8b4cfa4', '60bcd89e-e356-43d0-97d7-2fbff7b0bcc0', NULL, 0, '9e5e4e98-8037-4b00-9c15-93d83532ea57', 'admin', '2021-09-03 14:30:42.605694', 'admin', '2021-09-03 14:30:42.605694');
INSERT INTO `RoleMenus` VALUES (21, '81e2c6b0-6ab3-4fbb-923c-c9a1a8b4cfa4', 'dec6d8b2-20d5-4d70-ad24-3bc7d5bae64e', NULL, 0, '5c56936a-b37c-43e5-afdf-173785b471d5', 'admin', '2021-09-03 14:30:42.605694', 'admin', '2021-09-03 14:30:42.605694');
INSERT INTO `RoleMenus` VALUES (22, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', '655f0c99-8629-4439-8998-52b772b51e81', NULL, 0, '2dda67ae-4808-43a7-8036-dc23fdd2524f', '系统管理员', '2021-09-08 11:11:37.814176', '系统管理员', '2021-09-08 11:11:37.814176');
INSERT INTO `RoleMenus` VALUES (23, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', '58a78eff-e04f-461d-a464-463f0f363aaf', NULL, 0, 'd06ded2a-c22c-4aa6-ba45-22e5a227fdc8', '系统管理员', '2021-09-08 11:11:37.814176', '系统管理员', '2021-09-08 11:11:37.814176');
INSERT INTO `RoleMenus` VALUES (24, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', 'cb94bb18-8711-42ae-9af5-82d6c7977ef9', NULL, 0, 'b7c28000-588a-489e-88aa-ce797ffbc2db', '系统管理员', '2021-09-08 11:11:37.814176', '系统管理员', '2021-09-08 11:11:37.814176');
INSERT INTO `RoleMenus` VALUES (25, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', 'fad7d157-d31f-445e-b56e-6c00cf785bb5', NULL, 0, 'e36b4ba4-dc90-45ab-9a0d-9eb14c8d8b73', '系统管理员', '2021-09-08 11:11:37.814176', '系统管理员', '2021-09-08 11:11:37.814176');
INSERT INTO `RoleMenus` VALUES (26, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', '1db5112c-cce7-4da3-ab26-2abd8a1d2674', NULL, 0, '12a599e2-0fdf-4d33-a992-ad4446781dbb', '系统管理员', '2021-09-08 11:11:37.814176', '系统管理员', '2021-09-08 11:11:37.814176');
INSERT INTO `RoleMenus` VALUES (27, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', '60bcd89e-e356-43d0-97d7-2fbff7b0bcc0', NULL, 0, '76724321-c9f4-4c07-a9b8-7d4fcc1b153c', '系统管理员', '2021-09-08 11:11:37.814176', '系统管理员', '2021-09-08 11:11:37.814176');
INSERT INTO `RoleMenus` VALUES (28, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', 'dec6d8b2-20d5-4d70-ad24-3bc7d5bae64e', NULL, 0, '47de8ced-7084-4a9d-ac2f-0e30c4911165', '系统管理员', '2021-09-08 11:11:37.814176', '系统管理员', '2021-09-08 11:11:37.814176');
INSERT INTO `RoleMenus` VALUES (29, 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', 'aac7b3d5-619e-41bd-b036-bbbba2cdf62c', NULL, 0, '3d3cef2e-ee91-4eba-944e-670db335721f', '系统管理员', '2021-09-08 11:11:37.814176', '系统管理员', '2021-09-08 11:11:37.814176');

-- ----------------------------
-- Table structure for Roles
-- ----------------------------
DROP TABLE IF EXISTS `Roles`;
CREATE TABLE `Roles`  (
  `ClusterID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `IsDeleted` int(11) NOT NULL,
  `HalfCheckeds` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `ID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `CreateTime` datetime(6) NOT NULL,
  `UpdateUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `UpdateTime` datetime(6) NOT NULL,
  PRIMARY KEY (`ClusterID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Roles
-- ----------------------------
INSERT INTO `Roles` VALUES (1, '系统管理员', NULL, 0, '[]', 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', '系统管理员', '2021-08-26 14:14:19.000000', '系统管理员', '2021-09-08 11:11:37.770548');
INSERT INTO `Roles` VALUES (2, '测试', '', 1, '[]', 'd70f1763-04e0-418a-b381-22c7bb21a09b', 'admin', '2021-09-03 12:55:40.921322', 'admin', '2021-09-03 13:05:37.386346');
INSERT INTO `Roles` VALUES (3, '普通用户', '', 0, '[]', '81e2c6b0-6ab3-4fbb-923c-c9a1a8b4cfa4', 'admin', '2021-09-03 14:30:42.603185', 'admin', '2021-09-03 14:30:42.603185');

-- ----------------------------
-- Table structure for TokenInfos
-- ----------------------------
DROP TABLE IF EXISTS `TokenInfos`;
CREATE TABLE `TokenInfos`  (
  `ID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TokenMsg` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `LastLoginTime` datetime(6) NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of TokenInfos
-- ----------------------------
INSERT INTO `TokenInfos` VALUES ('446dc64b-139a-449a-99dc-ccf82589435d', '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IntcIkRhdGFcIjpcIkhJdWYzL1ExM0pieUZHUEdxYWNWY3Y0d3ExZzJTdTRVY1RYaUYzNjJURmlMSUFudUVybjkrZmFZeTE3a1JiUFdcIixcIlRpbWVzdGFtcFwiOjE2MzEwNjU0MDEsXCJTaWduXCI6XCJjZmYxMGZjMDBmZWJjOTViZDZmZDdiZWJmODRjNDU3NlwifSIsIm5iZiI6MTYzMTA2NTQwMiwiZXhwIjoxNjMxMDY1NTIyLCJpc3MiOiJFUE0uQXV0aGVudGljYXRpb24iLCJhdWQiOiJFUE0uQnVzaW5lc3MifQ.3tJLXdAB91_Q-yEWlJPi-P1dAyG9DnTpavofxukQkq8', '2021-09-08 09:43:22.665575');
INSERT INTO `TokenInfos` VALUES ('3eb8f276-6787-47d4-95be-fa37838c5466', '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IntcIkRhdGFcIjpcIkpMSG9MQWhhSGZCckxDWXQzaUNCS003SlVId3lua3gwZTlwRVRnOFVQR0V1cmQ2dnhtVFB1amwxZXZ1aUZVK1ZcIixcIlRpbWVzdGFtcFwiOjE2MzEwNjU3NDMsXCJTaWduXCI6XCJjZmYxMGZjMDBmZWJjOTViZDZmZDdiZWJmODRjNDU3NlwifSIsIm5iZiI6MTYzMTA2NTc0MywiZXhwIjoxNjMxMDY1ODYzLCJpc3MiOiJFUE0uQXV0aGVudGljYXRpb24iLCJhdWQiOiJFUE0uQnVzaW5lc3MifQ.fk7MuuUjxl9izFDduPQOdDPYsdxA0DMDgUdJ-GggVFc', '2021-09-08 09:49:03.540150');
INSERT INTO `TokenInfos` VALUES ('719b1806-bf63-4a58-b26a-636963a7e885', '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IntcIkRhdGFcIjpcIlp2T1ZKQm9KRnhKbTN1T0htNnkxQlZTa2Vrb2tlSDVOWnBqZGVjaWtLb3V2blNiWEVsMEZxL3dnUWpkeWgvVnpcIixcIlRpbWVzdGFtcFwiOjE2MzEwNjU4NzgsXCJTaWduXCI6XCJjZmYxMGZjMDBmZWJjOTViZDZmZDdiZWJmODRjNDU3NlwifSIsIm5iZiI6MTYzMTA2NTg3OCwiZXhwIjoxNjMxMDY1OTk4LCJpc3MiOiJFUE0uQXV0aGVudGljYXRpb24iLCJhdWQiOiJFUE0uQnVzaW5lc3MifQ.Fh0AlzisEyEv291Ijhlbfyi-FZJippT6L3OoDeSppHs', '2021-09-08 09:51:18.002751');
INSERT INTO `TokenInfos` VALUES ('f5225eca-b979-467c-acc6-77ed1cb2f498', '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', '{\"Data\":\"lHWmETpqFaT1JSrit90JvtXo3v573bFKE5+mYwjudQqnxWdrqe8aUFGPZ3664c1TsRGr1T5nYYdk8aibxhyHmJbp8FiauH1ifPVekBcmNrsKmQ/lLvOq6ggDaBT2/eK0y7ZdvT806ASqFfA6GEXliLkz8RDXir9BXRMEjOX+LgDDoFhoZNLmTI2FOv3jnhFNgIA9m96wlKP2jCVbMUn0wm/31UZ/bKJ6HhenpB0NqBIJPdvt3qmvyOP4JXTbKhR+074OkuHYVBJuyjVsuRhQJLyPwKAEjxNfqBvg7DeZCVKRmNMMnb174NATtQKAnpZVdDrlIOphbWKRgJHc8RZ67Q==\",\"Timestamp\":1631066161,\"Sign\":\"91a90334d37de37ef985ca6d7e527d31\"}', '2021-09-08 09:56:02.135171');
INSERT INTO `TokenInfos` VALUES ('061526d5-875a-49c6-8eac-eb088897e3ec', '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', '{\"Data\":\"zDUhwkj13fjxfcA77DBvhoHFzduxv4/yOzlPmbWTVFrYbY4ILTW8MmZuFQsdKbWF7jiVheVB2LtQlSsXyvaw3SH7gpbcrUubmVZHRoInTJPkybL83wB4UlR+54icITgo3giAYauQNEVf7hYGmG84Z0c/jY1hIX9ScKyxeyKfpzTVpV6t3bB+m7ZyWhPOZq6BVDzx0qw4z1LOhyRQukwxDeq5xReT+t3RA9XayzBdCJ7+oeN5l/aFnZ8RufxEzGU7lbLxsHi+HhXf4vFyGxmIB2F9bWqwOMkQ61f7M5GfAwU8sAQYBJk2nv/3RZfzfwZK+SAxhiBOKV5eyVKVk8R4pA==\",\"Timestamp\":1631066344,\"Sign\":\"669c3e641bcbacf33d29f6eb3d60f228\"}', '2021-09-08 09:59:04.861154');
INSERT INTO `TokenInfos` VALUES ('15e51cb4-4fba-41a7-a193-a9c02f7100f4', '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', '{\"Data\":\"zOyqDr1UqdO3c6zcuKvOJkOx8/LtlBhN22N27ZeP1KhizXCoDE4huclbYeRRpRhz35LEbVLaLwL0NymUPIKJOQ7mOG/wAhB8Ote56wZf/mVbqTTleF3/npoiNS39qjC3zc1r/2IMIZPk5jHmZDM5AOHbxVkquxNAMNjzvrqMHeXn/fv+JLzVp9713yAxCRX8GcArZLeOpyRwj8kwnltuNrXFsyyFArN6m2sd+HPWvh7mbyG9PqGdwSnPkdJCgPlSz26uDxmsoGsxrE5w3zkyEFnlteBzDJp2ZDaD2BbRmk9mbAqLgPP3JsXxNoF1+RD6LjV+3QkeVRZ88FtWYZtBiw==\",\"Timestamp\":1631066387,\"Sign\":\"0868a6bc5d26c9892ba5f8e3e4bc3379\"}', '2021-09-08 09:59:47.184075');
INSERT INTO `TokenInfos` VALUES ('dfda566a-7e5a-4ff5-aaa3-311c2ff19e4b', '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IntcIkRhdGFcIjpcIm5HQXhNa1BKRXRyTmRHUzZSMDVkM1NKUGpKZk9OWlFTK0RRa200TzNzTDB2eVBHQWVmb1hOL2hFRGNGajA4V3lcIixcIlRpbWVzdGFtcFwiOjE2MzEwNjgxMTcsXCJTaWduXCI6XCJjZmYxMGZjMDBmZWJjOTViZDZmZDdiZWJmODRjNDU3NlwifSIsIm5iZiI6MTYzMTA2ODExNywiZXhwIjoxNjMxMDY4MjM3LCJpc3MiOiJFUE0uQXV0aGVudGljYXRpb24iLCJhdWQiOiJFUE0uQnVzaW5lc3MifQ.GM4DJikjbGr4KNCBLi2nSpN5X6qGREKocF3M-lXs5Mg', '2021-09-08 10:28:38.359096');
INSERT INTO `TokenInfos` VALUES ('5ab3be4d-50a5-4f86-ab1e-3fb769fed7f3', '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IntcIkRhdGFcIjpcIjI2OUh4TXBGZUc3S2ZuRmROUHl1eVM4TGNib1BPbk1pNWRCRFllak1maFZLeHVVSUdyS2FTZTRSVmdoRUZKcUtcIixcIlRpbWVzdGFtcFwiOjE2MzEwNjgyMDMsXCJTaWduXCI6XCJjZmYxMGZjMDBmZWJjOTViZDZmZDdiZWJmODRjNDU3NlwifSIsIm5iZiI6MTYzMTA2ODIwMywiZXhwIjoxNjMxMDY4MzIzLCJpc3MiOiJFUE0uQXV0aGVudGljYXRpb24iLCJhdWQiOiJFUE0uQnVzaW5lc3MifQ.QAblNU8-Ha7gcKwKyDSZwClTWZgPSsCJtA06cGQMQKQ', '2021-09-08 10:30:03.824771');
INSERT INTO `TokenInfos` VALUES ('b402d1be-d99f-45cb-a704-b7221460582d', 'a2cc9e7b-320b-458c-8361-7911a3d01232', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IntcIkRhdGFcIjpcIjhpMGVFT04vKzlCMERGOVIzaGwyNlNtTTVmcytTMGV4T3N2KzNqUnZPU05tMHJrNldDYm1FY0o2b0VRVHFHdXhcIixcIlRpbWVzdGFtcFwiOjE2MzEwNjgyNDAsXCJTaWduXCI6XCJjNDc2OWFhMzExNjBhMDgzYmQ2ZjMwZWRjMGVkZmVjYlwifSIsIm5iZiI6MTYzMTA2ODI0MCwiZXhwIjoxNjMxMDY4MzYwLCJpc3MiOiJFUE0uQXV0aGVudGljYXRpb24iLCJhdWQiOiJFUE0uQnVzaW5lc3MifQ.-r6OUy4JebS-2Vubu7VSdZfg4WjSa2qwt89G0sNMjfA', '2021-09-08 10:30:40.912884');
INSERT INTO `TokenInfos` VALUES ('621de1e9-604a-4b73-bca3-ff52d2be9902', '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IntcIkRhdGFcIjpcIkd1M0NYSXVJclp4NFprZ3BVM2ZCc1I1bTVDTE9ZbXhDUzF3M3VyMXVXTmF4YzZtY3FhSVM5eUJ3dXZaT3lpc3ZcIixcIlRpbWVzdGFtcFwiOjE2MzEwNjgzNzksXCJTaWduXCI6XCJjZmYxMGZjMDBmZWJjOTViZDZmZDdiZWJmODRjNDU3NlwifSIsIm5iZiI6MTYzMTA2ODM3OSwiZXhwIjoxNjMxMDY4NDk5LCJpc3MiOiJFUE0uQXV0aGVudGljYXRpb24iLCJhdWQiOiJFUE0uQnVzaW5lc3MifQ.3BfMB38dCAADq6F1b676_wm-d3gB9sNG9iqO47PCvTo', '2021-09-08 10:32:59.409124');
INSERT INTO `TokenInfos` VALUES ('21fe2e17-5f0c-4188-a133-3b7182e99b8c', 'a2cc9e7b-320b-458c-8361-7911a3d01232', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IntcIkRhdGFcIjpcImZCRXFtUlNqendlOWhTNXdpaXVYY1pnSTZaWVJxTERGYVNxSjQ1WlM5ajFFWmpkbDRDS3gyMXQ3bnJiOGNpUUVcIixcIlRpbWVzdGFtcFwiOjE2MzEwNjg0MDAsXCJTaWduXCI6XCJjNDc2OWFhMzExNjBhMDgzYmQ2ZjMwZWRjMGVkZmVjYlwifSIsIm5iZiI6MTYzMTA2ODQwMCwiZXhwIjoxNjMxMDY4NTIwLCJpc3MiOiJFUE0uQXV0aGVudGljYXRpb24iLCJhdWQiOiJFUE0uQnVzaW5lc3MifQ.UWpDVPBsXh-7KkrWCR-YXFxORutE8c3qS6xY31m5YJI', '2021-09-08 10:33:20.959729');
INSERT INTO `TokenInfos` VALUES ('65354057-b327-4d05-a1f9-46c6555b86e2', '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IntcIkRhdGFcIjpcIjFPaTVFdVNBdHBiM0FzRUl1Q2UyTHNJUXNxNDRQbTlCV25kRTBjWW5XYkpjSUdIM1lDRWtXSEFOaTFvQ0VWU2JcIixcIlRpbWVzdGFtcFwiOjE2MzEwNzA2MjIsXCJTaWduXCI6XCJjZmYxMGZjMDBmZWJjOTViZDZmZDdiZWJmODRjNDU3NlwifSIsIm5iZiI6MTYzMTA3MDYyMywiZXhwIjoxNjMxMDcwNzQzLCJpc3MiOiJFUE0uQXV0aGVudGljYXRpb24iLCJhdWQiOiJFUE0uQnVzaW5lc3MifQ.1vSdHZTZXCt9Nw8bOD1moXn9Nl69GZU0GbSbmlzIm5k', '2021-09-08 11:10:23.615376');
INSERT INTO `TokenInfos` VALUES ('51893265-1b35-482b-8130-db448b56334e', '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IntcIkRhdGFcIjpcIjRFWXFVbmZVc0tHd0VGWUpmTW9hczZCK3JWQ0g1N2xqK1djSDA3bUFtbDM4VkVFUG14dXJhQ0lVMTM5K3czOENcIixcIlRpbWVzdGFtcFwiOjE2MzEwNzA3MDksXCJTaWduXCI6XCJjZmYxMGZjMDBmZWJjOTViZDZmZDdiZWJmODRjNDU3NlwifSIsIm5iZiI6MTYzMTA3MDcwOSwiZXhwIjoxNjMxMDcwODI5LCJpc3MiOiJFUE0uQXV0aGVudGljYXRpb24iLCJhdWQiOiJFUE0uQnVzaW5lc3MifQ.KERVZSj94vLy_O6Ti_L4znsZ0fsd1WYJ0CnfoqaIVh8', '2021-09-08 11:11:49.246958');
INSERT INTO `TokenInfos` VALUES ('1974f2b6-38b0-4df8-a12a-e1198da77361', '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IntcIkRhdGFcIjpcInh2QnIrVXN3cVFDQ0RZT2ZVZ1M0TVdIYjIwbHpsUitqV0I1aWhHNHJibFBIWThaWklMTUU3YWQvOERkNEJGTHRcIixcIlRpbWVzdGFtcFwiOjE2MzEwNzA5MDMsXCJTaWduXCI6XCJjZmYxMGZjMDBmZWJjOTViZDZmZDdiZWJmODRjNDU3NlwifSIsIm5iZiI6MTYzMTA3MDkwMywiZXhwIjoxNjMxMDcxMDIzLCJpc3MiOiJFUE0uQXV0aGVudGljYXRpb24iLCJhdWQiOiJFUE0uQnVzaW5lc3MifQ.utlVNIA5w1qsm0ZdwI4l771w_hlFifjOxgVZ_qi1Ldw', '2021-09-08 11:15:03.917964');
INSERT INTO `TokenInfos` VALUES ('444d7825-44e4-4a0a-b5fd-1477c8fc9fac', '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IntcIkRhdGFcIjpcImljbzYwT2NrcG13T0NPWno3U1pJWXhmRjhqUkdFVUdrelFIeStmVFJjbVd4eVlhUk1mM0F2TDUwb3BYT3IzNFdcIixcIlRpbWVzdGFtcFwiOjE2MzEwNzM1ODIsXCJTaWduXCI6XCJjZmYxMGZjMDBmZWJjOTViZDZmZDdiZWJmODRjNDU3NlwifSIsIm5iZiI6MTYzMTA3MzU4MiwiZXhwIjoxNjMxMDczNzAyLCJpc3MiOiJFUE0uQXV0aGVudGljYXRpb24iLCJhdWQiOiJFUE0uQnVzaW5lc3MifQ.qdNgp2lzEU0GlBvZUSlNDCiD66Af22Ijqr0UGV9C9o0', '2021-09-08 11:59:42.778007');
INSERT INTO `TokenInfos` VALUES ('4ee3a342-bddb-47a7-83bd-6f16d40d40d6', '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IntcIkRhdGFcIjpcImFScVppTnY4SDNQQ0JGZ0hCUjhYL3VJOXEzLzlqLzhSTFV2bkVMSlpBRkN3QjRDUVV1MTJIcFhCTm1hVjhQUmpcIixcIlRpbWVzdGFtcFwiOjE2MzEwNzQxODIsXCJTaWduXCI6XCJjZmYxMGZjMDBmZWJjOTViZDZmZDdiZWJmODRjNDU3NlwifSIsIm5iZiI6MTYzMTA3NDE4MiwiZXhwIjoxNjMxMDc0MzAyLCJpc3MiOiJFUE0uQXV0aGVudGljYXRpb24iLCJhdWQiOiJFUE0uQnVzaW5lc3MifQ.EJf6cIBEHKn_ULHDejllMmon5-5DYl518XLhB2n87nI', '2021-09-08 12:09:42.754190');

-- ----------------------------
-- Table structure for Users
-- ----------------------------
DROP TABLE IF EXISTS `Users`;
CREATE TABLE `Users`  (
  `ClusterID` int(11) NOT NULL AUTO_INCREMENT,
  `LoginName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `MobileNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `DepartmentID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `RoleID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `EmailAddress` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `Position` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `Status` int(11) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `LoginTime` datetime(6) NULL DEFAULT NULL,
  `PasswordUpdateTime` datetime(6) NULL DEFAULT NULL,
  `LoginErrorCount` int(11) NOT NULL,
  `LoginLockTime` datetime(6) NULL DEFAULT NULL,
  `ID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `CreateTime` datetime(6) NOT NULL,
  `UpdateUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `UpdateTime` datetime(6) NOT NULL,
  PRIMARY KEY (`ClusterID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Users
-- ----------------------------
INSERT INTO `Users` VALUES (1, 'admin', 'e10adc3949ba59abbe56e057f20f883e', '系统管理员', NULL, '7946dffb-209c-4d3d-88ed-df25c4eec325', 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', NULL, NULL, NULL, 0, 0, '2021-09-08 12:09:42.754330', NULL, 0, NULL, '44f2fbfc-6832-4157-b8c2-e9f7da3b6a65', '系统管理员', '2021-08-26 14:14:19.000000', '系统管理员', '2021-08-26 14:14:19.000000');
INSERT INTO `Users` VALUES (2, 'test', 'e10adc3949ba59abbe56e057f20f883e', '测试', '18745', 'e7b751de-0e2d-4e34-bed5-e3e3d21b32de', 'd10f7e9f-d075-443d-bcd3-ef6f64903d92', '', '', '', 0, 0, NULL, NULL, 0, NULL, '94417611-d1b8-41d0-a937-93ffeaa4f914', 'admin', '2021-09-03 13:47:08.505121', 'admin', '2021-09-03 13:47:08.505123');
INSERT INTO `Users` VALUES (3, 'jxl', 'e10adc3949ba59abbe56e057f20f883e', '姜晓亮', '', '3003eb64-b04d-445e-9ca1-3c3250bc2e4c', '81e2c6b0-6ab3-4fbb-923c-c9a1a8b4cfa4', '', '', '', 0, 0, '2021-09-08 10:33:20.959839', NULL, 0, NULL, 'a2cc9e7b-320b-458c-8361-7911a3d01232', 'admin', '2021-09-03 14:31:27.296528', 'admin', '2021-09-03 14:31:27.296528');

-- ----------------------------
-- Table structure for WorkItems
-- ----------------------------
DROP TABLE IF EXISTS `WorkItems`;
CREATE TABLE `WorkItems`  (
  `ClusterID` int(11) NOT NULL AUTO_INCREMENT,
  `WorkContent` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `RecordDate` datetime(6) NULL DEFAULT NULL,
  `ID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `CreateTime` datetime(6) NOT NULL,
  `UpdateUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `UpdateTime` datetime(6) NOT NULL,
  `CreateUserID` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `IsRecord` int(255) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ClusterID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 20 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of WorkItems
-- ----------------------------
INSERT INTO `WorkItems` VALUES (16, '啊飒飒的', '我强恶趣味', '2021-09-01 00:00:00.000000', '4702ce8d-eb99-44ef-97a0-7b419c33cbc4', '姜晓亮', '2021-09-07 18:10:01.367656', '姜晓亮', '2021-09-07 18:49:41.330599', 'a2cc9e7b-320b-458c-8361-7911a3d01232', 1);
INSERT INTO `WorkItems` VALUES (17, '213213123123', '', '2021-09-02 00:00:00.000000', 'dda1ce1b-9cb2-46e4-aeeb-257c42654d39', '姜晓亮', '2021-09-07 18:34:05.402245', '姜晓亮', '2021-09-07 18:34:05.402245', 'a2cc9e7b-320b-458c-8361-7911a3d01232', 1);

-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
DROP TABLE IF EXISTS `__EFMigrationsHistory`;
CREATE TABLE `__EFMigrationsHistory`  (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of __EFMigrationsHistory
-- ----------------------------
INSERT INTO `__EFMigrationsHistory` VALUES ('20210826061420_InitialDb', '5.0.9');

SET FOREIGN_KEY_CHECKS = 1;


CREATE TABLE `DataAuthorities`  (
  `ClusterID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
	  `DepartID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `IsDeleted` int(11) NOT NULL,
  `ID` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CreateUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `CreateTime` datetime(6) NOT NULL,
  `UpdateUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `UpdateTime` datetime(6) NOT NULL,
  PRIMARY KEY (`ClusterID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;
