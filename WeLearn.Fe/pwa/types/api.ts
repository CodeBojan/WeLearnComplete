//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.16.1.0 (NJsonSchema v10.7.2.0 (Newtonsoft.Json v11.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming



export class DeleteFollowedCourseDto implements IDeleteFollowedCourseDto {
    accountId?: string;
    courseId?: string;

    constructor(data?: IDeleteFollowedCourseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.accountId = _data["accountId"];
            this.courseId = _data["courseId"];
        }
    }

    static fromJS(data: any): DeleteFollowedCourseDto {
        data = typeof data === 'object' ? data : {};
        let result = new DeleteFollowedCourseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["accountId"] = this.accountId;
        data["courseId"] = this.courseId;
        return data;
    }
}

export interface IDeleteFollowedCourseDto {
    accountId?: string;
    courseId?: string;
}

export class DeleteFollowedStudyYearDto implements IDeleteFollowedStudyYearDto {
    accountId?: string;
    studyYearId?: string;

    constructor(data?: IDeleteFollowedStudyYearDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.accountId = _data["accountId"];
            this.studyYearId = _data["studyYearId"];
        }
    }

    static fromJS(data: any): DeleteFollowedStudyYearDto {
        data = typeof data === 'object' ? data : {};
        let result = new DeleteFollowedStudyYearDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["accountId"] = this.accountId;
        data["studyYearId"] = this.studyYearId;
        return data;
    }
}

export interface IDeleteFollowedStudyYearDto {
    accountId?: string;
    studyYearId?: string;
}

export class GetAccountDto implements IGetAccountDto {
    id?: string;
    username?: string | undefined;
    email?: string | undefined;
    firstName?: string | undefined;
    lastName?: string | undefined;
    facultyStudentId?: string | undefined;

    constructor(data?: IGetAccountDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.username = _data["username"];
            this.email = _data["email"];
            this.firstName = _data["firstName"];
            this.lastName = _data["lastName"];
            this.facultyStudentId = _data["facultyStudentId"];
        }
    }

    static fromJS(data: any): GetAccountDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetAccountDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["username"] = this.username;
        data["email"] = this.email;
        data["firstName"] = this.firstName;
        data["lastName"] = this.lastName;
        data["facultyStudentId"] = this.facultyStudentId;
        return data;
    }
}

export interface IGetAccountDto {
    id?: string;
    username?: string | undefined;
    email?: string | undefined;
    firstName?: string | undefined;
    lastName?: string | undefined;
    facultyStudentId?: string | undefined;
}

export class GetAccountDtoPagedResponseDto implements IGetAccountDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetAccountDto[] | undefined;

    constructor(data?: IGetAccountDtoPagedResponseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.limit = _data["limit"];
            this.page = _data["page"];
            this.totalPages = _data["totalPages"];
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetAccountDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): GetAccountDtoPagedResponseDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetAccountDtoPagedResponseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["limit"] = this.limit;
        data["page"] = this.page;
        data["totalPages"] = this.totalPages;
        if (Array.isArray(this.data)) {
            data["data"] = [];
            for (let item of this.data)
                data["data"].push(item.toJSON());
        }
        return data;
    }
}

export interface IGetAccountDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetAccountDto[] | undefined;
}

export class GetCourseDto implements IGetCourseDto {
    id?: string;
    code?: string | undefined;
    shortName?: string | undefined;
    fullName?: string | undefined;
    staff?: string | undefined;
    description?: string | undefined;
    rules?: string | undefined;
    studyYearId?: string;
    createdDate?: Date;
    updatedDate?: Date;
    followingCount?: number | undefined;
    isFollowing?: boolean | undefined;

    constructor(data?: IGetCourseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.code = _data["code"];
            this.shortName = _data["shortName"];
            this.fullName = _data["fullName"];
            this.staff = _data["staff"];
            this.description = _data["description"];
            this.rules = _data["rules"];
            this.studyYearId = _data["studyYearId"];
            this.createdDate = _data["createdDate"] ? new Date(_data["createdDate"].toString()) : <any>undefined;
            this.updatedDate = _data["updatedDate"] ? new Date(_data["updatedDate"].toString()) : <any>undefined;
            this.followingCount = _data["followingCount"];
            this.isFollowing = _data["isFollowing"];
        }
    }

    static fromJS(data: any): GetCourseDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetCourseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["code"] = this.code;
        data["shortName"] = this.shortName;
        data["fullName"] = this.fullName;
        data["staff"] = this.staff;
        data["description"] = this.description;
        data["rules"] = this.rules;
        data["studyYearId"] = this.studyYearId;
        data["createdDate"] = this.createdDate ? this.createdDate.toISOString() : <any>undefined;
        data["updatedDate"] = this.updatedDate ? this.updatedDate.toISOString() : <any>undefined;
        data["followingCount"] = this.followingCount;
        data["isFollowing"] = this.isFollowing;
        return data;
    }
}

export interface IGetCourseDto {
    id?: string;
    code?: string | undefined;
    shortName?: string | undefined;
    fullName?: string | undefined;
    staff?: string | undefined;
    description?: string | undefined;
    rules?: string | undefined;
    studyYearId?: string;
    createdDate?: Date;
    updatedDate?: Date;
    followingCount?: number | undefined;
    isFollowing?: boolean | undefined;
}

export class GetCourseDtoPagedResponseDto implements IGetCourseDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetCourseDto[] | undefined;

    constructor(data?: IGetCourseDtoPagedResponseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.limit = _data["limit"];
            this.page = _data["page"];
            this.totalPages = _data["totalPages"];
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetCourseDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): GetCourseDtoPagedResponseDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetCourseDtoPagedResponseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["limit"] = this.limit;
        data["page"] = this.page;
        data["totalPages"] = this.totalPages;
        if (Array.isArray(this.data)) {
            data["data"] = [];
            for (let item of this.data)
                data["data"].push(item.toJSON());
        }
        return data;
    }
}

export interface IGetCourseDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetCourseDto[] | undefined;
}

export class GetCourseMaterialUploadRequestDto implements IGetCourseMaterialUploadRequestDto {

    constructor(data?: IGetCourseMaterialUploadRequestDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
    }

    static fromJS(data: any): GetCourseMaterialUploadRequestDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetCourseMaterialUploadRequestDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        return data;
    }
}

export interface IGetCourseMaterialUploadRequestDto {
}

export class GetCourseMaterialUploadRequestDtoPagedResponseDto implements IGetCourseMaterialUploadRequestDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetCourseMaterialUploadRequestDto[] | undefined;

    constructor(data?: IGetCourseMaterialUploadRequestDtoPagedResponseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.limit = _data["limit"];
            this.page = _data["page"];
            this.totalPages = _data["totalPages"];
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetCourseMaterialUploadRequestDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): GetCourseMaterialUploadRequestDtoPagedResponseDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetCourseMaterialUploadRequestDtoPagedResponseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["limit"] = this.limit;
        data["page"] = this.page;
        data["totalPages"] = this.totalPages;
        if (Array.isArray(this.data)) {
            data["data"] = [];
            for (let item of this.data)
                data["data"].push(item.toJSON());
        }
        return data;
    }
}

export interface IGetCourseMaterialUploadRequestDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetCourseMaterialUploadRequestDto[] | undefined;
}

export class GetFeedDto implements IGetFeedDto {

    constructor(data?: IGetFeedDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
    }

    static fromJS(data: any): GetFeedDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetFeedDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        return data;
    }
}

export interface IGetFeedDto {
}

export class GetFeedDtoPagedResponseDto implements IGetFeedDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetFeedDto[] | undefined;

    constructor(data?: IGetFeedDtoPagedResponseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.limit = _data["limit"];
            this.page = _data["page"];
            this.totalPages = _data["totalPages"];
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetFeedDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): GetFeedDtoPagedResponseDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetFeedDtoPagedResponseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["limit"] = this.limit;
        data["page"] = this.page;
        data["totalPages"] = this.totalPages;
        if (Array.isArray(this.data)) {
            data["data"] = [];
            for (let item of this.data)
                data["data"].push(item.toJSON());
        }
        return data;
    }
}

export interface IGetFeedDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetFeedDto[] | undefined;
}

export class GetFollowedCourseDto implements IGetFollowedCourseDto {
    accountId?: string;
    courseId?: string;
    courseShortName?: string | undefined;
    courseFullName?: string | undefined;
    courseCode?: string | undefined;

    constructor(data?: IGetFollowedCourseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.accountId = _data["accountId"];
            this.courseId = _data["courseId"];
            this.courseShortName = _data["courseShortName"];
            this.courseFullName = _data["courseFullName"];
            this.courseCode = _data["courseCode"];
        }
    }

    static fromJS(data: any): GetFollowedCourseDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetFollowedCourseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["accountId"] = this.accountId;
        data["courseId"] = this.courseId;
        data["courseShortName"] = this.courseShortName;
        data["courseFullName"] = this.courseFullName;
        data["courseCode"] = this.courseCode;
        return data;
    }
}

export interface IGetFollowedCourseDto {
    accountId?: string;
    courseId?: string;
    courseShortName?: string | undefined;
    courseFullName?: string | undefined;
    courseCode?: string | undefined;
}

export class GetFollowedCourseDtoPagedResponseDto implements IGetFollowedCourseDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetFollowedCourseDto[] | undefined;

    constructor(data?: IGetFollowedCourseDtoPagedResponseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.limit = _data["limit"];
            this.page = _data["page"];
            this.totalPages = _data["totalPages"];
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetFollowedCourseDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): GetFollowedCourseDtoPagedResponseDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetFollowedCourseDtoPagedResponseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["limit"] = this.limit;
        data["page"] = this.page;
        data["totalPages"] = this.totalPages;
        if (Array.isArray(this.data)) {
            data["data"] = [];
            for (let item of this.data)
                data["data"].push(item.toJSON());
        }
        return data;
    }
}

export interface IGetFollowedCourseDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetFollowedCourseDto[] | undefined;
}

export class GetFollowedStudyYearDto implements IGetFollowedStudyYearDto {
    accountId?: string;
    studyYearId?: string;

    constructor(data?: IGetFollowedStudyYearDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.accountId = _data["accountId"];
            this.studyYearId = _data["studyYearId"];
        }
    }

    static fromJS(data: any): GetFollowedStudyYearDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetFollowedStudyYearDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["accountId"] = this.accountId;
        data["studyYearId"] = this.studyYearId;
        return data;
    }
}

export interface IGetFollowedStudyYearDto {
    accountId?: string;
    studyYearId?: string;
}

export class GetFollowedStudyYearDtoPagedResponseDto implements IGetFollowedStudyYearDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetFollowedStudyYearDto[] | undefined;

    constructor(data?: IGetFollowedStudyYearDtoPagedResponseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.limit = _data["limit"];
            this.page = _data["page"];
            this.totalPages = _data["totalPages"];
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetFollowedStudyYearDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): GetFollowedStudyYearDtoPagedResponseDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetFollowedStudyYearDtoPagedResponseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["limit"] = this.limit;
        data["page"] = this.page;
        data["totalPages"] = this.totalPages;
        if (Array.isArray(this.data)) {
            data["data"] = [];
            for (let item of this.data)
                data["data"].push(item.toJSON());
        }
        return data;
    }
}

export interface IGetFollowedStudyYearDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetFollowedStudyYearDto[] | undefined;
}

export class GetNotificationDto implements IGetNotificationDto {

    constructor(data?: IGetNotificationDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
    }

    static fromJS(data: any): GetNotificationDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetNotificationDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        return data;
    }
}

export interface IGetNotificationDto {
}

export class GetNotificationDtoPagedResponseDto implements IGetNotificationDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetNotificationDto[] | undefined;

    constructor(data?: IGetNotificationDtoPagedResponseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.limit = _data["limit"];
            this.page = _data["page"];
            this.totalPages = _data["totalPages"];
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetNotificationDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): GetNotificationDtoPagedResponseDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetNotificationDtoPagedResponseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["limit"] = this.limit;
        data["page"] = this.page;
        data["totalPages"] = this.totalPages;
        if (Array.isArray(this.data)) {
            data["data"] = [];
            for (let item of this.data)
                data["data"].push(item.toJSON());
        }
        return data;
    }
}

export interface IGetNotificationDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetNotificationDto[] | undefined;
}

export class GetStudyYearDto implements IGetStudyYearDto {
    readonly id?: string;
    readonly createdDate?: Date;
    readonly updatedDate?: Date;
    readonly shortName?: string | undefined;
    fullName?: string | undefined;
    description?: string | undefined;
    followingCount?: number | undefined;
    isFollowing?: boolean | undefined;

    constructor(data?: IGetStudyYearDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            (<any>this).id = _data["id"];
            (<any>this).createdDate = _data["createdDate"] ? new Date(_data["createdDate"].toString()) : <any>undefined;
            (<any>this).updatedDate = _data["updatedDate"] ? new Date(_data["updatedDate"].toString()) : <any>undefined;
            (<any>this).shortName = _data["shortName"];
            this.fullName = _data["fullName"];
            this.description = _data["description"];
            this.followingCount = _data["followingCount"];
            this.isFollowing = _data["isFollowing"];
        }
    }

    static fromJS(data: any): GetStudyYearDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetStudyYearDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["createdDate"] = this.createdDate ? this.createdDate.toISOString() : <any>undefined;
        data["updatedDate"] = this.updatedDate ? this.updatedDate.toISOString() : <any>undefined;
        data["shortName"] = this.shortName;
        data["fullName"] = this.fullName;
        data["description"] = this.description;
        data["followingCount"] = this.followingCount;
        data["isFollowing"] = this.isFollowing;
        return data;
    }
}

export interface IGetStudyYearDto {
    id?: string;
    createdDate?: Date;
    updatedDate?: Date;
    shortName?: string | undefined;
    fullName?: string | undefined;
    description?: string | undefined;
    followingCount?: number | undefined;
    isFollowing?: boolean | undefined;
}

export class GetStudyYearDtoPagedResponseDto implements IGetStudyYearDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetStudyYearDto[] | undefined;

    constructor(data?: IGetStudyYearDtoPagedResponseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.limit = _data["limit"];
            this.page = _data["page"];
            this.totalPages = _data["totalPages"];
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetStudyYearDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): GetStudyYearDtoPagedResponseDto {
        data = typeof data === 'object' ? data : {};
        let result = new GetStudyYearDtoPagedResponseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["limit"] = this.limit;
        data["page"] = this.page;
        data["totalPages"] = this.totalPages;
        if (Array.isArray(this.data)) {
            data["data"] = [];
            for (let item of this.data)
                data["data"].push(item.toJSON());
        }
        return data;
    }
}

export interface IGetStudyYearDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | undefined;
    data?: GetStudyYearDto[] | undefined;
}

export class PostCourseDto implements IPostCourseDto {
    code?: string | undefined;
    shortName?: string | undefined;
    fullName?: string | undefined;
    staff?: string | undefined;
    description?: string | undefined;
    rules?: string | undefined;
    studyYearId?: string;

    constructor(data?: IPostCourseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.code = _data["code"];
            this.shortName = _data["shortName"];
            this.fullName = _data["fullName"];
            this.staff = _data["staff"];
            this.description = _data["description"];
            this.rules = _data["rules"];
            this.studyYearId = _data["studyYearId"];
        }
    }

    static fromJS(data: any): PostCourseDto {
        data = typeof data === 'object' ? data : {};
        let result = new PostCourseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["code"] = this.code;
        data["shortName"] = this.shortName;
        data["fullName"] = this.fullName;
        data["staff"] = this.staff;
        data["description"] = this.description;
        data["rules"] = this.rules;
        data["studyYearId"] = this.studyYearId;
        return data;
    }
}

export interface IPostCourseDto {
    code?: string | undefined;
    shortName?: string | undefined;
    fullName?: string | undefined;
    staff?: string | undefined;
    description?: string | undefined;
    rules?: string | undefined;
    studyYearId?: string;
}

export class PostFollowedCourseDto implements IPostFollowedCourseDto {
    accountId?: string;
    courseId?: string;

    constructor(data?: IPostFollowedCourseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.accountId = _data["accountId"];
            this.courseId = _data["courseId"];
        }
    }

    static fromJS(data: any): PostFollowedCourseDto {
        data = typeof data === 'object' ? data : {};
        let result = new PostFollowedCourseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["accountId"] = this.accountId;
        data["courseId"] = this.courseId;
        return data;
    }
}

export interface IPostFollowedCourseDto {
    accountId?: string;
    courseId?: string;
}

export class PostFollowedStudyYearDto implements IPostFollowedStudyYearDto {
    accountId?: string;
    studyYearId?: string;

    constructor(data?: IPostFollowedStudyYearDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.accountId = _data["accountId"];
            this.studyYearId = _data["studyYearId"];
        }
    }

    static fromJS(data: any): PostFollowedStudyYearDto {
        data = typeof data === 'object' ? data : {};
        let result = new PostFollowedStudyYearDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["accountId"] = this.accountId;
        data["studyYearId"] = this.studyYearId;
        return data;
    }
}

export interface IPostFollowedStudyYearDto {
    accountId?: string;
    studyYearId?: string;
}

export class PutAccountDto implements IPutAccountDto {
    firstName?: string | undefined;
    lastName?: string | undefined;
    facultyStudentId?: string | undefined;

    constructor(data?: IPutAccountDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.firstName = _data["firstName"];
            this.lastName = _data["lastName"];
            this.facultyStudentId = _data["facultyStudentId"];
        }
    }

    static fromJS(data: any): PutAccountDto {
        data = typeof data === 'object' ? data : {};
        let result = new PutAccountDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["firstName"] = this.firstName;
        data["lastName"] = this.lastName;
        data["facultyStudentId"] = this.facultyStudentId;
        return data;
    }
}

export interface IPutAccountDto {
    firstName?: string | undefined;
    lastName?: string | undefined;
    facultyStudentId?: string | undefined;
}

export class PutCourseDto implements IPutCourseDto {
    code?: string | undefined;
    shortName?: string | undefined;
    fullName?: string | undefined;
    staff?: string | undefined;
    description?: string | undefined;
    rules?: string | undefined;

    constructor(data?: IPutCourseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.code = _data["code"];
            this.shortName = _data["shortName"];
            this.fullName = _data["fullName"];
            this.staff = _data["staff"];
            this.description = _data["description"];
            this.rules = _data["rules"];
        }
    }

    static fromJS(data: any): PutCourseDto {
        data = typeof data === 'object' ? data : {};
        let result = new PutCourseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["code"] = this.code;
        data["shortName"] = this.shortName;
        data["fullName"] = this.fullName;
        data["staff"] = this.staff;
        data["description"] = this.description;
        data["rules"] = this.rules;
        return data;
    }
}

export interface IPutCourseDto {
    code?: string | undefined;
    shortName?: string | undefined;
    fullName?: string | undefined;
    staff?: string | undefined;
    description?: string | undefined;
    rules?: string | undefined;
}

export class PutStudyYearDto implements IPutStudyYearDto {
    shortName?: string | undefined;
    fullName?: string | undefined;
    description?: string | undefined;

    constructor(data?: IPutStudyYearDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.shortName = _data["shortName"];
            this.fullName = _data["fullName"];
            this.description = _data["description"];
        }
    }

    static fromJS(data: any): PutStudyYearDto {
        data = typeof data === 'object' ? data : {};
        let result = new PutStudyYearDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["shortName"] = this.shortName;
        data["fullName"] = this.fullName;
        data["description"] = this.description;
        return data;
    }
}

export interface IPutStudyYearDto {
    shortName?: string | undefined;
    fullName?: string | undefined;
    description?: string | undefined;
}

export class WeatherForecast implements IWeatherForecast {
    date?: Date;
    temperatureC?: number;
    readonly temperatureF?: number;
    summary?: string | undefined;

    constructor(data?: IWeatherForecast) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.date = _data["date"] ? new Date(_data["date"].toString()) : <any>undefined;
            this.temperatureC = _data["temperatureC"];
            (<any>this).temperatureF = _data["temperatureF"];
            this.summary = _data["summary"];
        }
    }

    static fromJS(data: any): WeatherForecast {
        data = typeof data === 'object' ? data : {};
        let result = new WeatherForecast();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["date"] = this.date ? this.date.toISOString() : <any>undefined;
        data["temperatureC"] = this.temperatureC;
        data["temperatureF"] = this.temperatureF;
        data["summary"] = this.summary;
        return data;
    }
}

export interface IWeatherForecast {
    date?: Date;
    temperatureC?: number;
    temperatureF?: number;
    summary?: string | undefined;
}