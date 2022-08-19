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
            this.accountId = _data["accountId"] !== undefined ? _data["accountId"] : <any>null;
            this.courseId = _data["courseId"] !== undefined ? _data["courseId"] : <any>null;
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
        data["accountId"] = this.accountId !== undefined ? this.accountId : <any>null;
        data["courseId"] = this.courseId !== undefined ? this.courseId : <any>null;
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
            this.accountId = _data["accountId"] !== undefined ? _data["accountId"] : <any>null;
            this.studyYearId = _data["studyYearId"] !== undefined ? _data["studyYearId"] : <any>null;
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
        data["accountId"] = this.accountId !== undefined ? this.accountId : <any>null;
        data["studyYearId"] = this.studyYearId !== undefined ? this.studyYearId : <any>null;
        return data;
    }
}

export interface IDeleteFollowedStudyYearDto {
    accountId?: string;
    studyYearId?: string;
}

export class GetAccountDto implements IGetAccountDto {
    id?: string;
    username?: string | null;
    email?: string | null;
    firstName?: string | null;
    lastName?: string | null;
    facultyStudendId?: string | null;

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
            this.id = _data["id"] !== undefined ? _data["id"] : <any>null;
            this.username = _data["username"] !== undefined ? _data["username"] : <any>null;
            this.email = _data["email"] !== undefined ? _data["email"] : <any>null;
            this.firstName = _data["firstName"] !== undefined ? _data["firstName"] : <any>null;
            this.lastName = _data["lastName"] !== undefined ? _data["lastName"] : <any>null;
            this.facultyStudendId = _data["facultyStudendId"] !== undefined ? _data["facultyStudendId"] : <any>null;
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
        data["id"] = this.id !== undefined ? this.id : <any>null;
        data["username"] = this.username !== undefined ? this.username : <any>null;
        data["email"] = this.email !== undefined ? this.email : <any>null;
        data["firstName"] = this.firstName !== undefined ? this.firstName : <any>null;
        data["lastName"] = this.lastName !== undefined ? this.lastName : <any>null;
        data["facultyStudendId"] = this.facultyStudendId !== undefined ? this.facultyStudendId : <any>null;
        return data;
    }
}

export interface IGetAccountDto {
    id?: string;
    username?: string | null;
    email?: string | null;
    firstName?: string | null;
    lastName?: string | null;
    facultyStudendId?: string | null;
}

export class GetAccountDtoPagedResponseDto implements IGetAccountDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | null;
    data?: GetAccountDto[] | null;

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
            this.limit = _data["limit"] !== undefined ? _data["limit"] : <any>null;
            this.page = _data["page"] !== undefined ? _data["page"] : <any>null;
            this.totalPages = _data["totalPages"] !== undefined ? _data["totalPages"] : <any>null;
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetAccountDto.fromJS(item));
            }
            else {
                this.data = <any>null;
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
        data["limit"] = this.limit !== undefined ? this.limit : <any>null;
        data["page"] = this.page !== undefined ? this.page : <any>null;
        data["totalPages"] = this.totalPages !== undefined ? this.totalPages : <any>null;
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
    totalPages?: number | null;
    data?: GetAccountDto[] | null;
}

export class GetCourseDto implements IGetCourseDto {
    id?: string;
    code?: string | null;
    shortName?: string | null;
    fullName?: string | null;
    staff?: string | null;
    description?: string | null;
    rules?: string | null;
    studyYearId?: string;

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
            this.id = _data["id"] !== undefined ? _data["id"] : <any>null;
            this.code = _data["code"] !== undefined ? _data["code"] : <any>null;
            this.shortName = _data["shortName"] !== undefined ? _data["shortName"] : <any>null;
            this.fullName = _data["fullName"] !== undefined ? _data["fullName"] : <any>null;
            this.staff = _data["staff"] !== undefined ? _data["staff"] : <any>null;
            this.description = _data["description"] !== undefined ? _data["description"] : <any>null;
            this.rules = _data["rules"] !== undefined ? _data["rules"] : <any>null;
            this.studyYearId = _data["studyYearId"] !== undefined ? _data["studyYearId"] : <any>null;
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
        data["id"] = this.id !== undefined ? this.id : <any>null;
        data["code"] = this.code !== undefined ? this.code : <any>null;
        data["shortName"] = this.shortName !== undefined ? this.shortName : <any>null;
        data["fullName"] = this.fullName !== undefined ? this.fullName : <any>null;
        data["staff"] = this.staff !== undefined ? this.staff : <any>null;
        data["description"] = this.description !== undefined ? this.description : <any>null;
        data["rules"] = this.rules !== undefined ? this.rules : <any>null;
        data["studyYearId"] = this.studyYearId !== undefined ? this.studyYearId : <any>null;
        return data;
    }
}

export interface IGetCourseDto {
    id?: string;
    code?: string | null;
    shortName?: string | null;
    fullName?: string | null;
    staff?: string | null;
    description?: string | null;
    rules?: string | null;
    studyYearId?: string;
}

export class GetCourseDtoPagedResponseDto implements IGetCourseDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | null;
    data?: GetCourseDto[] | null;

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
            this.limit = _data["limit"] !== undefined ? _data["limit"] : <any>null;
            this.page = _data["page"] !== undefined ? _data["page"] : <any>null;
            this.totalPages = _data["totalPages"] !== undefined ? _data["totalPages"] : <any>null;
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetCourseDto.fromJS(item));
            }
            else {
                this.data = <any>null;
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
        data["limit"] = this.limit !== undefined ? this.limit : <any>null;
        data["page"] = this.page !== undefined ? this.page : <any>null;
        data["totalPages"] = this.totalPages !== undefined ? this.totalPages : <any>null;
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
    totalPages?: number | null;
    data?: GetCourseDto[] | null;
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
    totalPages?: number | null;
    data?: GetCourseMaterialUploadRequestDto[] | null;

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
            this.limit = _data["limit"] !== undefined ? _data["limit"] : <any>null;
            this.page = _data["page"] !== undefined ? _data["page"] : <any>null;
            this.totalPages = _data["totalPages"] !== undefined ? _data["totalPages"] : <any>null;
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetCourseMaterialUploadRequestDto.fromJS(item));
            }
            else {
                this.data = <any>null;
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
        data["limit"] = this.limit !== undefined ? this.limit : <any>null;
        data["page"] = this.page !== undefined ? this.page : <any>null;
        data["totalPages"] = this.totalPages !== undefined ? this.totalPages : <any>null;
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
    totalPages?: number | null;
    data?: GetCourseMaterialUploadRequestDto[] | null;
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
    totalPages?: number | null;
    data?: GetFeedDto[] | null;

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
            this.limit = _data["limit"] !== undefined ? _data["limit"] : <any>null;
            this.page = _data["page"] !== undefined ? _data["page"] : <any>null;
            this.totalPages = _data["totalPages"] !== undefined ? _data["totalPages"] : <any>null;
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetFeedDto.fromJS(item));
            }
            else {
                this.data = <any>null;
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
        data["limit"] = this.limit !== undefined ? this.limit : <any>null;
        data["page"] = this.page !== undefined ? this.page : <any>null;
        data["totalPages"] = this.totalPages !== undefined ? this.totalPages : <any>null;
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
    totalPages?: number | null;
    data?: GetFeedDto[] | null;
}

export class GetFollowedCourseDto implements IGetFollowedCourseDto {
    accountId?: string;
    courseId?: string;
    courseShortName?: string | null;
    courseFullName?: string | null;
    courseCode?: string | null;

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
            this.accountId = _data["accountId"] !== undefined ? _data["accountId"] : <any>null;
            this.courseId = _data["courseId"] !== undefined ? _data["courseId"] : <any>null;
            this.courseShortName = _data["courseShortName"] !== undefined ? _data["courseShortName"] : <any>null;
            this.courseFullName = _data["courseFullName"] !== undefined ? _data["courseFullName"] : <any>null;
            this.courseCode = _data["courseCode"] !== undefined ? _data["courseCode"] : <any>null;
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
        data["accountId"] = this.accountId !== undefined ? this.accountId : <any>null;
        data["courseId"] = this.courseId !== undefined ? this.courseId : <any>null;
        data["courseShortName"] = this.courseShortName !== undefined ? this.courseShortName : <any>null;
        data["courseFullName"] = this.courseFullName !== undefined ? this.courseFullName : <any>null;
        data["courseCode"] = this.courseCode !== undefined ? this.courseCode : <any>null;
        return data;
    }
}

export interface IGetFollowedCourseDto {
    accountId?: string;
    courseId?: string;
    courseShortName?: string | null;
    courseFullName?: string | null;
    courseCode?: string | null;
}

export class GetFollowedCourseDtoPagedResponseDto implements IGetFollowedCourseDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | null;
    data?: GetFollowedCourseDto[] | null;

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
            this.limit = _data["limit"] !== undefined ? _data["limit"] : <any>null;
            this.page = _data["page"] !== undefined ? _data["page"] : <any>null;
            this.totalPages = _data["totalPages"] !== undefined ? _data["totalPages"] : <any>null;
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetFollowedCourseDto.fromJS(item));
            }
            else {
                this.data = <any>null;
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
        data["limit"] = this.limit !== undefined ? this.limit : <any>null;
        data["page"] = this.page !== undefined ? this.page : <any>null;
        data["totalPages"] = this.totalPages !== undefined ? this.totalPages : <any>null;
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
    totalPages?: number | null;
    data?: GetFollowedCourseDto[] | null;
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
            this.accountId = _data["accountId"] !== undefined ? _data["accountId"] : <any>null;
            this.studyYearId = _data["studyYearId"] !== undefined ? _data["studyYearId"] : <any>null;
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
        data["accountId"] = this.accountId !== undefined ? this.accountId : <any>null;
        data["studyYearId"] = this.studyYearId !== undefined ? this.studyYearId : <any>null;
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
    totalPages?: number | null;
    data?: GetFollowedStudyYearDto[] | null;

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
            this.limit = _data["limit"] !== undefined ? _data["limit"] : <any>null;
            this.page = _data["page"] !== undefined ? _data["page"] : <any>null;
            this.totalPages = _data["totalPages"] !== undefined ? _data["totalPages"] : <any>null;
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetFollowedStudyYearDto.fromJS(item));
            }
            else {
                this.data = <any>null;
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
        data["limit"] = this.limit !== undefined ? this.limit : <any>null;
        data["page"] = this.page !== undefined ? this.page : <any>null;
        data["totalPages"] = this.totalPages !== undefined ? this.totalPages : <any>null;
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
    totalPages?: number | null;
    data?: GetFollowedStudyYearDto[] | null;
}

export class GetStudyYearDto implements IGetStudyYearDto {
    readonly id?: string;
    readonly createdDate?: Date;
    readonly updatedDate?: Date;
    readonly shortName?: string | null;
    fullName?: string | null;
    description?: string | null;
    following?: number | null;

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
            (<any>this).id = _data["id"] !== undefined ? _data["id"] : <any>null;
            (<any>this).createdDate = _data["createdDate"] ? new Date(_data["createdDate"].toString()) : <any>null;
            (<any>this).updatedDate = _data["updatedDate"] ? new Date(_data["updatedDate"].toString()) : <any>null;
            (<any>this).shortName = _data["shortName"] !== undefined ? _data["shortName"] : <any>null;
            this.fullName = _data["fullName"] !== undefined ? _data["fullName"] : <any>null;
            this.description = _data["description"] !== undefined ? _data["description"] : <any>null;
            this.following = _data["following"] !== undefined ? _data["following"] : <any>null;
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
        data["id"] = this.id !== undefined ? this.id : <any>null;
        data["createdDate"] = this.createdDate ? this.createdDate.toISOString() : <any>null;
        data["updatedDate"] = this.updatedDate ? this.updatedDate.toISOString() : <any>null;
        data["shortName"] = this.shortName !== undefined ? this.shortName : <any>null;
        data["fullName"] = this.fullName !== undefined ? this.fullName : <any>null;
        data["description"] = this.description !== undefined ? this.description : <any>null;
        data["following"] = this.following !== undefined ? this.following : <any>null;
        return data;
    }
}

export interface IGetStudyYearDto {
    id?: string;
    createdDate?: Date;
    updatedDate?: Date;
    shortName?: string | null;
    fullName?: string | null;
    description?: string | null;
    following?: number | null;
}

export class GetStudyYearDtoPagedResponseDto implements IGetStudyYearDtoPagedResponseDto {
    limit?: number;
    page?: number;
    totalPages?: number | null;
    data?: GetStudyYearDto[] | null;

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
            this.limit = _data["limit"] !== undefined ? _data["limit"] : <any>null;
            this.page = _data["page"] !== undefined ? _data["page"] : <any>null;
            this.totalPages = _data["totalPages"] !== undefined ? _data["totalPages"] : <any>null;
            if (Array.isArray(_data["data"])) {
                this.data = [] as any;
                for (let item of _data["data"])
                    this.data!.push(GetStudyYearDto.fromJS(item));
            }
            else {
                this.data = <any>null;
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
        data["limit"] = this.limit !== undefined ? this.limit : <any>null;
        data["page"] = this.page !== undefined ? this.page : <any>null;
        data["totalPages"] = this.totalPages !== undefined ? this.totalPages : <any>null;
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
    totalPages?: number | null;
    data?: GetStudyYearDto[] | null;
}

export class PostCourseDto implements IPostCourseDto {
    code?: string | null;
    shortName?: string | null;
    fullName?: string | null;
    staff?: string | null;
    description?: string | null;
    rules?: string | null;
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
            this.code = _data["code"] !== undefined ? _data["code"] : <any>null;
            this.shortName = _data["shortName"] !== undefined ? _data["shortName"] : <any>null;
            this.fullName = _data["fullName"] !== undefined ? _data["fullName"] : <any>null;
            this.staff = _data["staff"] !== undefined ? _data["staff"] : <any>null;
            this.description = _data["description"] !== undefined ? _data["description"] : <any>null;
            this.rules = _data["rules"] !== undefined ? _data["rules"] : <any>null;
            this.studyYearId = _data["studyYearId"] !== undefined ? _data["studyYearId"] : <any>null;
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
        data["code"] = this.code !== undefined ? this.code : <any>null;
        data["shortName"] = this.shortName !== undefined ? this.shortName : <any>null;
        data["fullName"] = this.fullName !== undefined ? this.fullName : <any>null;
        data["staff"] = this.staff !== undefined ? this.staff : <any>null;
        data["description"] = this.description !== undefined ? this.description : <any>null;
        data["rules"] = this.rules !== undefined ? this.rules : <any>null;
        data["studyYearId"] = this.studyYearId !== undefined ? this.studyYearId : <any>null;
        return data;
    }
}

export interface IPostCourseDto {
    code?: string | null;
    shortName?: string | null;
    fullName?: string | null;
    staff?: string | null;
    description?: string | null;
    rules?: string | null;
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
            this.accountId = _data["accountId"] !== undefined ? _data["accountId"] : <any>null;
            this.courseId = _data["courseId"] !== undefined ? _data["courseId"] : <any>null;
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
        data["accountId"] = this.accountId !== undefined ? this.accountId : <any>null;
        data["courseId"] = this.courseId !== undefined ? this.courseId : <any>null;
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
            this.accountId = _data["accountId"] !== undefined ? _data["accountId"] : <any>null;
            this.studyYearId = _data["studyYearId"] !== undefined ? _data["studyYearId"] : <any>null;
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
        data["accountId"] = this.accountId !== undefined ? this.accountId : <any>null;
        data["studyYearId"] = this.studyYearId !== undefined ? this.studyYearId : <any>null;
        return data;
    }
}

export interface IPostFollowedStudyYearDto {
    accountId?: string;
    studyYearId?: string;
}

export class PutCourseDto implements IPutCourseDto {
    code?: string | null;
    shortName?: string | null;
    fullName?: string | null;
    staff?: string | null;
    description?: string | null;
    rules?: string | null;

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
            this.code = _data["code"] !== undefined ? _data["code"] : <any>null;
            this.shortName = _data["shortName"] !== undefined ? _data["shortName"] : <any>null;
            this.fullName = _data["fullName"] !== undefined ? _data["fullName"] : <any>null;
            this.staff = _data["staff"] !== undefined ? _data["staff"] : <any>null;
            this.description = _data["description"] !== undefined ? _data["description"] : <any>null;
            this.rules = _data["rules"] !== undefined ? _data["rules"] : <any>null;
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
        data["code"] = this.code !== undefined ? this.code : <any>null;
        data["shortName"] = this.shortName !== undefined ? this.shortName : <any>null;
        data["fullName"] = this.fullName !== undefined ? this.fullName : <any>null;
        data["staff"] = this.staff !== undefined ? this.staff : <any>null;
        data["description"] = this.description !== undefined ? this.description : <any>null;
        data["rules"] = this.rules !== undefined ? this.rules : <any>null;
        return data;
    }
}

export interface IPutCourseDto {
    code?: string | null;
    shortName?: string | null;
    fullName?: string | null;
    staff?: string | null;
    description?: string | null;
    rules?: string | null;
}

export class PutStudyYearDto implements IPutStudyYearDto {
    shortName?: string | null;
    fullName?: string | null;
    description?: string | null;

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
            this.shortName = _data["shortName"] !== undefined ? _data["shortName"] : <any>null;
            this.fullName = _data["fullName"] !== undefined ? _data["fullName"] : <any>null;
            this.description = _data["description"] !== undefined ? _data["description"] : <any>null;
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
        data["shortName"] = this.shortName !== undefined ? this.shortName : <any>null;
        data["fullName"] = this.fullName !== undefined ? this.fullName : <any>null;
        data["description"] = this.description !== undefined ? this.description : <any>null;
        return data;
    }
}

export interface IPutStudyYearDto {
    shortName?: string | null;
    fullName?: string | null;
    description?: string | null;
}

export class WeatherForecast implements IWeatherForecast {
    date?: Date;
    temperatureC?: number;
    readonly temperatureF?: number;
    summary?: string | null;

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
            this.date = _data["date"] ? new Date(_data["date"].toString()) : <any>null;
            this.temperatureC = _data["temperatureC"] !== undefined ? _data["temperatureC"] : <any>null;
            (<any>this).temperatureF = _data["temperatureF"] !== undefined ? _data["temperatureF"] : <any>null;
            this.summary = _data["summary"] !== undefined ? _data["summary"] : <any>null;
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
        data["date"] = this.date ? this.date.toISOString() : <any>null;
        data["temperatureC"] = this.temperatureC !== undefined ? this.temperatureC : <any>null;
        data["temperatureF"] = this.temperatureF !== undefined ? this.temperatureF : <any>null;
        data["summary"] = this.summary !== undefined ? this.summary : <any>null;
        return data;
    }
}

export interface IWeatherForecast {
    date?: Date;
    temperatureC?: number;
    temperatureF?: number;
    summary?: string | null;
}